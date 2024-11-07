using Markdig;
using Markdig.Syntax;
using Markdig.Extensions.Yaml;
using YamlDotNet.Serialization;
using System.Text.Json;
using System.Text;

namespace SearchBench;

internal static class BlogsManager
{
    internal static void ParseBlogs()
    {
        var path = Path.GetFullPath("./Content");
        var files = Directory.GetFiles(path);
        var mdPipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .UseYamlFrontMatter()
        .Build();
        JsonObject matters = new();

        for (int i = 0; i < files.Length; i++)
        {
            var file = files[i];
            var markdownContent = File.ReadAllText(file,Encoding.UTF8);
            var document = Markdown.Parse(markdownContent, mdPipeline);
            IDeserializer yamlDeserializer = new DeserializerBuilder()
                .IgnoreUnmatchedProperties()
                .Build();
            var yamlBlock = document.Descendants<YamlFrontMatterBlock>().FirstOrDefault();
            string frontMatterYaml = yamlBlock.Lines.ToString();
            var x = yamlDeserializer.Deserialize<FrontMatter>(frontMatterYaml);

            matters.url.Add(file.Replace(Directory.GetCurrentDirectory(), ""));
            matters.content[
                x.title?.ToLower() + "*-*" + // some fluff to separate the title from the rest
                x.description?.ToLower() +
                string.Join(' ', x.tags)?.ToLower() +
                x.keywords?.ToLower() +
                x.series?.ToLower() +
                markdownContent.Substring(markdownContent.IndexOf("---", 9)).Trim().Replace("  ", "")
            ] = i;
        }

        string jsonString = JsonSerializer.Serialize(matters);
        File.WriteAllText(Path.Combine(Path.GetFullPath("wwwroot"), "data", "include.json"), jsonString);
    }
}

class FrontMatter
{
    public string title;
    public string description;
    public DateTime date;
    public string[] tags;
    public string keywords;
    public string series;
}

class JsonObject
{
    public List<string> url { get; set; } = new();
    public Dictionary<string, int> content { get; set; } = new();
}
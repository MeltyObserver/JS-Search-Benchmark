### how to use it?

just run the app using `dotnet run` then click "run benchmark"

### how does it work?

each search functions is called a total of 7000 times (1000 time for each keyword) while measuring it's time to execute using c#

### where did you get the functions from?

the inverted index search function was *borrowed* from the [abrdige theme](<https://abridge.pages.dev/>)

as for the include function i wrote it myself

the functions can be found under `wwwroot/js`

### where's the data coming from?

at first i wanted to generate my own random json files, but honestly i didn't know how to generate an inverted index object so i just downloaded abridge's json file (`wwwroot/data/inverse.json`).

as for the other json for the include function that's generated on runtime in `ParseBlogs.cs` using the same blogs abridge have (located under `Content`) (the files were also converted to use yaml because there's no c# toml deserializer)

### data structure

my own solution uses a simple hash map like approach, basically you have a list of all posts (called them `url` here) and a dictionary with a key that contains ALL the data of post (metadata + body)

```cs
class JsonObject
{
    public List<string> url { get; set; } = new();
    public Dictionary<string, int> content { get; set; } = new();
}
```
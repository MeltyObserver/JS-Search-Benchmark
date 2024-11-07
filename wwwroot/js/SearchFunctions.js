// minify -> https://www.digitalocean.com/community/tools/minify
//! remove calls to console
var InverseData;
async function InvertedIndex(input) {
  var inputTrimmed = input.trim();
  var results = (await (InverseData = void 0 === InverseData ? fetch("data/inverse.json")
    .then((async function (t) { return elasticlunr.Index.load(await t.json()) })) : InverseData))
    .search(inputTrimmed, { bool: "OR", fields: { title: { boost: 2 }, body: { boost: 1 } } });
  console.log("inverse: " + results.length);
  return results.length
}

var data = 0;
async function SimpleInclude(input) {
  let inputTrimmed = input.trim().toLowerCase();
  if (inputTrimmed.length <= 2) return;
  if (data === 0) {
    data = await fetch('data/include.json')
      .then(response => {
        return response.json();
      });
  }
  var results = [];
  for (let key in data.content)
    if (data.content.hasOwnProperty(key) && key.includes(inputTrimmed)) {
      // let index = data.content[key];
      results.push(key);
    }
  console.log("include: " + results.length);
  return results.length;
}
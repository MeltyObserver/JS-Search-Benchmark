var count = 1000;
var inputs = ["lightweight", "Shortcodes", "iframe", "class", "Embed", "load", "xyz"];
export async function TestInclude() {
    for (let t = 0; t < inputs.length; t++) {
        for (let e = 0; e < count; e++) {
            await SimpleInclude(inputs[t])
        }
    }
}

export async function TestInverse() {
    for (let t = 0; t < inputs.length; t++) {
        for (let e = 0; e < count; e++) {
            await InvertedIndex(inputs[t])
        }
    }
}
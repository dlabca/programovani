let randomList = createRandomList();

let sortedListA = createSortedList();
let sortedListB = createSortedList();

function createRandomList(length = 20, max = 30) {
    let out = [];
    for (let i = 0; i < length; i++) {
        out.push(Math.floor(Math.random() * max));
    }
    return out;
}

function createSortedList(length = 10, max = 30) {
    let out = createRandomList(length, max);
    return out.sort((a, b) => a - b);
}
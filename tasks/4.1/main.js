
// Třída Cell reprezentuje jedno čtvercové pole ve čtvercové síti
// Potřebujeme
//      1. Možnost vytvořit novou buňku na daném místě a uložit její pozici
//         do proměnných 'x' a 'y', resp. 'this.x' a 'this.y'
//      2. Funkci getNeighbor, která vrátí náhodnou sousední buňku, která má proměnnou .visited nastavenou na false
//         Pokud žádná taková buňka neexistuje, funkce vrátí false

// Třída Cell by tedy měla mít 3 fieldy (proměnné): x, y a visited
//      (visited nemusíte přidávat sami, ale můžete, jenom to nastavte na začátku na false)

// K dispozici máte globální funkci getCell(x, y), která vrátí buňku na pozici (x, y).
//      pokud na pozici (x, y) žádná buňka není, funkce vrátí false
// Není to nikde potřeba, ale pokud byste chtěli, máte také k dispozici globální proměnné gridW a gridH,
//      které reprezentují rozměry čtvercové sítě

// Další užitečné funkce:
//      array.push(x) - přidá x do seznamu array
//      random(array) - vrátí náhodnou položku ze seznamu array
//      array.length - délka seznamu array

class Cell {
    constructor(x, y) {
        this.x = x;
        this.y = y;
    }

    getNeighbor() {
        let neighbors = [];
        let right = getCell(this.x + 1, this.y);
        let left = getCell(this.x - 1, this.y);
        let bot = getCell(this.x, this.y + 1);
        let top = getCell(this.x, this.y - 1);
        if (right && !right.visited)
            neighbors.push(right);
        if (left && !left.visited)
            neighbors.push(left);
        if (bot && !bot.visited)
            neighbors.push(bot);
        if (top && !top.visited)
            neighbors.push(top);

        if (neighbors.length == 0) return false;

        return random(neighbors);
    }
}


// Doplňte funkci follow, aby posunula všechny body ze seznamu objects směrem k bodu target
// Chceme posunout každý bod vždy o stejnou vzdálenost (např. 3)
// Proměnná objects je seznam vektorů a proměnná target je vektor
// Užitečné funkce:
//      - p5.Vector.add(a, b) - sečte vektory a a b
//          - vrátí tedy vektor (a + b)
//      - p5.Vector.sub(a, b) - odečte vektor b od vektoru a
//          - vrátí tedy vektor (a - b), což je směrový vektor z bodu b do bodu a
//      - vector.mult(x) - vynásobí vektor číslem x (prodlouží ho x-krát)
//      - vector.div(x) - vydělí vektor číslem x (zkrátí ho x-krát)
//      - vector.add(v) - přičte k vektoru vektor v
//      - vector.sub(v) - odečte od vektoru vektor v
//      - vector.setMag(x) - nastaví délku vektoru na x
//      - for(const x of list) - udělá něco pro každou položku v seznamu list a vždy nastaví hodnotu této položky do proměnné x
//          - (x se může jmenovat jak chceme) 
//          - na místě 'const' může být i 'let' nebo 'var', v tomto případě na tom moc nezáleží
function follow(objects, target) {
    let dir = createVector(1, 0);
    for (const object of objects) {
        object.add(dir);
    }
}
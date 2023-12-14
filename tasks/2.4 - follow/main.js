

// Doplňte funkci follow, aby posunula bod object směrem k bodu target
// a vrátila novou pozici bodu object
// Chceme posunout object vždy o stejnou vzdálenost (např. 3)
// Proměnné object a target jsou vektory
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
function follow(object, target) {
    let dir = createVector(1, 0);
    object.add(dir);
    return object;
}
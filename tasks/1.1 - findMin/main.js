function setup() {
    noCanvas();

    print(`List: ${randomList}`);

    print(`Min: ${findMin(randomList)}`);
}

// Doplňte funkci findMin, aby vrátila nejmenší číslo z daného seznamu
// (až otevřete program v prohlížeči, F12 vám otevře konzoli kde uvidíte jaký seznam váš program dostal a jaké číslo vrátil)
// Užitečné funkce:
//      list[i] - položka v seznamu na pozici i
//      list.length - délka seznamu
//      for(let i = 0; i < max; i++) - udělá něco max-krát, do proměnné i nastaví postupně čísla od 0 do (max - 1)
//      return x - vrátí z funkce hodnotu x
function findMin(list) {
    var max;
    var a = 100;
    max = list.length;
    for(let i = 0; i < max; i++) {
        if(a > list[i]){
            a = list[i]
        }
    }
    return a
}
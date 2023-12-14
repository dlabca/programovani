function setup() {
    noCanvas();

    print(`List: ${randomList}`);

    print(`Sorted: ${mergesort(randomList)}`);
}

// Doplňte funkci mergesort, aby rekurzivně setřídila seznam
// Princip je jednoduchý:
//      - abychom setřídili seznam, stačí nám setřídit zvlášť jeho levou polovinu a
//        zvlášť jeho pravou polovinu a slít obě setřízené poloviny pomocí naší merge funkce
//      - na setřízení každé poloviny můžeme znovu použít funkci mergesort (která tedy setřídí poloviny každé poloviny atd.)
//      - seznam, který má pouze jeden prvek je vždy setřízený
// Trik je v tom že funkce může zavolat sama sebe
// Tomu se říká rekurze
// Je to často velmi jednoduchý způsob jak naprogramovat něco složitého, ale pozor na to aby opakované volání funkce někdy skončilo
// Je velmi lehké pomocí rekurze vytvořit program, který nikdy neskončí
function mergesort(list) {
    return list;
}
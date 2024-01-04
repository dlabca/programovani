function setup() {
    noCanvas();

    print(`List: ${randomList}`);

    print(`Sorted: ${mergesort(randomList)}`);
}

function merge(listA, listB) {
    let out = [];
    
    var vetsilist = listA.length + listB.length
    for(var p = 0;p < vetsilist; p++){
        if(listB.length == 0 || (listA.length > 0 && listA[0]<listB[0])){
            out.push(listA[0]);
            listA.splice(0, 1);
        }
        else {
            out.push(listB[0]);
            listB.splice(0, 1);
        }
    }
    return out;
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
    if (list.length <= 1)
        return list
    
    let left = list.slice(0, list.length/2)
    let right = list.slice(list.length/2, list.length)

    left = mergesort(left)
    right = mergesort(right)

    return merge(left, right)
}
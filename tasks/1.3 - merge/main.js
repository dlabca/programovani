function setup() {
    noCanvas();

    print(`ListA: ${sortedListA}`);
    print(`ListB: ${sortedListB}`);

    print(`Merged: ${merge(sortedListA, sortedListB)}`);
}

// Funkce merge dostance dva setřízené seznamy.
// Doplňte ji, aby seznamy 'slila' do jednoho setřízeného seznamu.
// Není potřeba nic znovu třídit, stačí přidávat do výsledného seznamu ve správném pořadí
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
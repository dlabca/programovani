// Globální proměnné
let randomAngle1;
let randomAngle2;
let odpoved = 0;
let angleBetween = 0;
let angle;
let pokracovat = false;
let uspechy = 0;
let neuspechy = 0;
let ignorovanyrozdil = 0;
let pocether = 0;
let procentauspesnosti = 0;
let randomRadius = 300;
let controler = 0;

// Nastavení canvasu
function setup() {
    createCanvas(1525, 650);
    background(220);
    rectMode(CORNER);
    kresleni();
}

// Hlavní vykreslovací funkce
function draw() {
    if (odpoved > 0) {
        pocether++;
        background(220);
        vykresliLinie();
        zobrazStatistiky();
        zobrazOdpoved();
        odpoved = 0;
    }
    if (pokracovat) {
        pokracovat = false;
        background(220);
        kresleni();
        zobrazStatistiky();
    }
    vypoctiVysledek();
}

// Funkce pro vykreslení náhodných čar a výpočet úhlu
function kresleni() {
    // Generování náhodných úhlů pro dvě čáry
    randomAngle1 = floor(random(0, 361));
    randomAngle2 = floor(random(0, 361));
    
    // Vykreslení první čáry (červená)
    stroke(255, 0, 0);
    line(width / 2, height / 2, width / 2 + randomRadius * cos(radians(randomAngle1)), height / 2 + randomRadius * sin(radians(randomAngle1)));
    
    // Vykreslení druhé čáry (černá)
    stroke(0, 0, 0);
    line(width / 2, height / 2, width / 2 + randomRadius * cos(radians(randomAngle2)), height / 2 + randomRadius * sin(radians(randomAngle2)));
    stroke(0, 0);
    
    // Výpočet úhlu mezi čarami
    angle = abs(randomAngle1 - randomAngle2);
    if (angle > 180) {
        angle = 360 - angle;
    }
}

// Zobrazení statistik
function zobrazStatistiky() {
    fill(0);
    textSize(20);
    text(`Úspěchy: ${uspechy}`, 10, 500);
    text(`Neúspěchy: ${neuspechy}`, 10, 525);
    text(`Počet her: ${pocether}`, 10, 550);
    procentauspesnosti = floor((uspechy / pocether) * 100);
    text(`Úspěšnost: ${procentauspesnosti} %`, 10, 575);
}

// Zobrazení odpovědi a rozdílu úhlu
function zobrazOdpoved() {
    fill(0);
    textSize(20);
    text(`Úhel mezi čarami: ${angle}°`, 10, 450);
    
    // Výpočet rozdílu mezi odhadem a skutečným úhlem
    angleBetween = Math.abs(angle - odpoved);
    text(`Rozdíl mezi vaším odhadem a menším úhlem: ${angleBetween}°`, 10, 475);
    
    // Vyhodnocení, zda je rozdíl menší nebo roven ignorovanému rozdílu
    if (angleBetween <= ignorovanyrozdil) {
        uspechy++;
    } else {
        neuspechy++;
    }
}

// Funkce pro uložení odpovědi z formuláře
function storeAnswer() {
    if (controler === 0) {
        answerInput = document.getElementById("answerInput");
        odpoved = parseInt(answerInput.value);
        controler = 1;
    }
}

// Pokračování na další pokus
function Pokracovat() {
    if (controler === 1) {
        pokracovat = true;
        controler = 0;
    }
}

// Načtení hodnoty ignorovaného rozdílu z formuláře
function vypoctiVysledek() {
    let vysledek = document.getElementById("ignorovanyrozdil");
    ignorovanyrozdil = parseInt(vysledek.value);
}

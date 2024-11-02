let randomAngle1;
let randomAngle2;
let odpoved = 0;
let angleBetween = 0;
let angle;
let pokracovat;
let uspechy = 0;
let neuspechy = 0;
let ignorovanyrozdil = 0;
let pocether = 0;
let procentauspesnosti;
let randomRadius = 300;
let vysledek;
let answerInput;
let controler = 0;
function setup() {
    createCanvas(1525, 650);
    background(220);
    rectMode(CORNER);
    kresleni();
}

function draw() {
    if (odpoved > 0) {
        pocether++;
        background(220);
        Restart();
        Text2();
        Text1();

        odpoved = 0;

    }
    if (pokracovat == 1) {
        pokracovat = 0;
        background(220);
        kresleni();
        Text1()

    }
    Vysledek()
}
function Restart() {
    stroke(255, 0, 0);
    line(width / 2, height / 2, width / 2 + randomRadius * cos(radians(randomAngle1)), height / 2 + randomRadius * sin(radians(randomAngle1)));
    stroke(0, 0, 0);
    line(width / 2, height / 2, width / 2 + randomRadius * cos(radians(randomAngle2)), height / 2 + randomRadius * sin(radians(randomAngle2)));
    stroke(0, 0);

}
function kresleni() {

    randomAngle1 = floor(random(0, 361));
    stroke(255, 0, 0);
    line(width / 2, height / 2, width / 2 + randomRadius * cos(radians(randomAngle1)), height / 2 + randomRadius * sin(radians(randomAngle1)));
    randomAngle2 = floor(random(0, 361));
    stroke(0, 0, 0);
    line(width / 2, height / 2, width / 2 + randomRadius * cos(radians(randomAngle2)), height / 2 + randomRadius * sin(radians(randomAngle2)));
    stroke(0, 0);

    // Po nakreslení obou čar vypočítáme úhel
    angle = abs(randomAngle1 - randomAngle2);
    if (angle > 180) {
        angle = 360 - angle;
    }

}
function Text1() {
    fill(0);
    textSize(20);
    text(`Úspěchy: ${uspechy}`, 10, 500);
    text(`Neúspěchy: ${neuspechy}`, 10, 525);
    text(`počet her: ${pocether}`, 10, 550);
    procentauspesnosti = floor(uspechy / pocether * 100);
    text(`úspěšnost: ${procentauspesnosti} %`, 10, 575);


}
function Text2() {
    fill(0);
    textSize(20);

    text(`Úhel mezi čarami: ${angle}°`, 10, 450);
    angleBetween = angle - odpoved;
    if (angleBetween < 0) {
        angleBetween = angleBetween * -1;
    }
    text(`Rozdíl mezi vaším odhadem a menším úhlem: ${angleBetween}°`, 10, 475);
    if (angleBetween <= ignorovanyrozdil) {
        uspechy = uspechy + 1;
    } else {
        neuspechy = neuspechy + 1;
    }

}
function storeAnswer() {
    if (controler == 0) {
        answerInput = document.getElementById("answerInput");
        odpoved = parseInt(answerInput.value);
        controler = 1;
    }
}
function Pokracovat() {
    if (controler == 1) {
        pokracovat = 1;
        controler = 0;
    }
}
function Vysledek() {
    vysledek = document.getElementById("ignorovanyrozdil");
    ignorovanyrozdil = parseInt(vysledek.value);
}
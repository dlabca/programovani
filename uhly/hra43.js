let randomAngle1;
let randomAngle2;
let odpoved = 0;
let angleBetween = 0;
let angle;
let pokracovat;
let uspechy = 0;
let neuspechy = 0;
let ignorovanyrozdil = 0;
function setup() {
    createCanvas(400, 540);
    background(220);
    rectMode(CORNER);
    kresleni();


}

function draw() {
    if (odpoved > 0) {
        Text1();
        odpoved = 0;
    }
    if (pokracovat == 1) {
        pokracovat = 0
        background(220);
        kresleni();

    }
    vysledek()
}

function kresleni() {
    let randomRadius = 200;
    randomAngle1 = floor(random(0, 361));
    stroke(255, 0, 0);
    line(200, 200, 200 + randomRadius * cos(radians(randomAngle1)), 200 + randomRadius * sin(radians(randomAngle1)));
    randomAngle2 = floor(random(0, 361));
    stroke(0, 0, 0);
    line(200, 200, 200 + randomRadius * cos(radians(randomAngle2)), 200 + randomRadius * sin(radians(randomAngle2)));

    stroke(0, 0);
    fill(200);
    rect(0, height - 140, width, 140);

    // Po nakreslení obou čar vypočítáme úhel
    angle = abs(randomAngle1 - randomAngle2);
    if (angle > 180) {
        angle = 360 - angle;
    }
}
function Text1() {
    fill(0);
    textSize(15);
    text(`Úhel mezi čarami: ${angle}°`, 10, 425);
    text(`Váš odhad: ${odpoved}°`, 10, 450);
    angleBetween = angle - odpoved;
    if (angleBetween < 0) {
        angleBetween = angleBetween * -1;
    }
    text(`Rozdíl: ${angleBetween}°`, 10, 475);
    if(angleBetween <= ignorovanyrozdil){uspechy = uspechy + 1}else{neuspechy = neuspechy +1}
    text(`Úspěchy: ${uspechy}`, 10, 500);
    text(`Neúspěchy: ${neuspechy}`, 10, 525);

}
function storeAnswer() {
    let answerInput = document.getElementById("answerInput");
    odpoved = parseInt(answerInput.value);
}
function Pokracovat() {
    pokracovat = 1;
}
function vysledek() {
    let vysledek = document.getElementById("ignorovanyrozdil");
    ignorovanyrozdil = parseInt(vysledek.value);
}
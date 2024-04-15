let angle = 0;
let randomAngle = 0;
let randomAngle2 = 0;
function setup() {
    createCanvas(400, 500);
    background(220);
    rectMode(CORNER);
}
function draw() {
    randomcislo();
    kresleni();

}
function randomcislo() {
    randomAngle = floor(random(0, 361));
    randomAngle2 = floor(random(0, 361));
}
function kresleni() {
    stroke(255, 0, 0);
    line(200, 200, 200 * cos(radians(randomAngle)), height / 2 * sin(radians(randomAngle)));
    line(200, 200, 200 * cos(radians(randomAngle2)), height / 2 * sin(radians(randomAngle2)));
    stroke(0, 0, 0)
    fill(200);
    rect(0, height - 100, width, 100);
}
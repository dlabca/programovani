let randomAngle2 = 0;
function setup() {
    createCanvas(400, 500);
    background(220);
    rectMode(CORNER);
        randomcislo();
    kresleni();
}
function draw() {


}
function randomcislo() {
}
function kresleni() {
    let randomRadius = 200;
    let randomAngle = floor(random(0, 361));
    let angle = abs(randomAngle - randomAngle2);

    stroke(255, 0, 0);
    line(200, 200, 200 + randomRadius * cos(radians(randomAngle)), 200 + randomRadius * sin(radians(randomAngle)));

    randomAngle2 = floor(random(0, 361));
    line(200, 200, 200 + randomRadius * cos(radians(randomAngle2)), 200 + randomRadius * sin(radians(randomAngle2)));

    stroke(0, 0, 0)
    fill(200);
    rect(0, height - 100, width, 100);

//    circle(200,200,400)

    console.log("Úhel mezi čarami je: " + angle);
}
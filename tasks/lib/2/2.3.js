let center;
const length = 100;

function setup() {
    createCanvas(400, 400);

    center = createVector(width / 2, height / 2);
}

function draw() {
    background(220);

    let mouse = createVector(mouseX, mouseY);
    let dir = p5.Vector.sub(mouse, center);
    dir.setMag(length);
    let fixedPoint = p5.Vector.add(center, dir);
    
    stroke(0);
    drawLine(center, mouse, length);
    
    noStroke();
    fill(255, 0, 0);
    circle(fixedPoint.x, fixedPoint.y, 5);
}
let center;

function setup() {
    createCanvas(400, 400);

    center = createVector(width / 2, height / 2);
}

function draw() {
    background(220);

    let mouse = createVector(mouseX, mouseY);
    let dir = p5.Vector.sub(mouse, center);
    dir.mult(0.5);
    let midpoint = p5.Vector.add(center, dir);
    
    stroke(0);
    drawLine(center, mouse);
    
    noStroke();
    fill(255, 0, 0);
    circle(midpoint.x, midpoint.y, 5);
}
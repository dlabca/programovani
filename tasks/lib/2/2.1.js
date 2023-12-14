let center;

function setup() {
    createCanvas(400, 400);

    center = createVector(width / 2, height / 2);
}

function draw() {
    background(220);

    let mouse = createVector(mouseX, mouseY);
    
    drawLine(center, mouse);
}
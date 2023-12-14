let entity;

function setup() {
    createCanvas(400, 400);

    entity = createVector(width / 2, height / 2);
}

function draw() {
    background(220);

    let mouse = createVector(mouseX, mouseY);
    
    entity.set(follow(entity, mouse));
    
    noStroke();
    fill(255, 0, 0);
    circle(entity.x, entity.y, 10);
}
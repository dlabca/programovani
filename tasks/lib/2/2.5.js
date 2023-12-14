let entities = [];

function setup() {
    createCanvas(400, 400);

    for (let i = 0; i < 10; i++) {
        entities.push(createVector(random() * width, random() * height));
    }
}

function draw() {
    background(220);

    let mouse = createVector(mouseX, mouseY);
    
    follow(entities, mouse);
    
    for (const entity of entities) {
        noStroke();
        fill(255, 0, 0);
        circle(entity.x, entity.y, 10);
    }
}
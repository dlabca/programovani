
function setup() {
    createCanvas(400, 400);
    background(51);

    Engine.start();
}

function draw() {
    background(51);

    Engine.update();
}
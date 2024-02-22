
function setup() {
    size(800, 600);

    new GameObject(width / 2, height - 5)
    .addComponent(new RectRenderer(width, 10, () => {
        noStroke(0);
        fill(0, 255, 0);
    }))
    .addComponent(new BoxCollider(width, 10))
}

function draw() {
    background(51);
}
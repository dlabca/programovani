
function setup() {
    createCanvas(400, 400);
    background(51);

    new GameObject(width/2,height+5)
    .addComponent(new BoxCollider(400,10))

    new GameObject(width+5,height/2)
    .addComponent(new BoxCollider(10,400))

    new GameObject(width/2,-5)
    .addComponent(new BoxCollider(400,10))

    new GameObject(-5,height/2)
    .addComponent(new BoxCollider(10,400))

    new GameObject(width/2, height/2)
    .addComponent(new RectRenderer(20, 20))
    .addComponent(new DynamicBoxCollider(20, 20))

    Engine.start();
}

function draw() {
    background(51);

    Engine.update();
}
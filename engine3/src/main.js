class Player extends Component {
    start() {
        this.rb = this.getComponent(DynamicBoxCollider);
    }
    update() {
        if(Input.keyJustPressed("w"))
            this.rb.vel.y = -10
        
        if(Input.keyPressed("a"))
            this.rb.vel.x = -3
        else if(Input.keyPressed("d"))
            this.rb.vel.x = +3
        else this.rb.vel.x = 0
        
    }
}
function setup() {
    size(1500, 700);
    background(51);
    new GameObject(width/2, height/2+10)
    .addComponent(new BoxCollider(50,10))
    .addComponent(new RectRenderer(50,10))

    new GameObject(width/2,height+5)
    .addComponent(new BoxCollider(width,10))

    new GameObject(width+5,height/2)
    .addComponent(new BoxCollider(10,height))

    new GameObject(width/2,-5)
    .addComponent(new BoxCollider(width,10))

    new GameObject(-5,height/2)
    .addComponent(new BoxCollider(10,height))

    new GameObject(width/2, height/2)
    .addComponent(new RectRenderer(20, 20))
    .addComponent(new DynamicBoxCollider(20, 20))
    .addComponent(new Player());
}

function draw() {
    background(51);
}
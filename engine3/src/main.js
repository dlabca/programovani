let camera = true;
let player;
class Player extends Component {

    start() {
        this.rb = this.getComponent(DynamicBoxCollider);
        this.grounded = false
        this.conterjump = 0;

        this.rb.onCollisionEnter = (col) => {
            if (col.hit.normal.y == -1)
                this.grounded = true;
        }

        this.rb.onCollisionExit = (col) => {
            if (col.hit.normal.y == -1)
                this.grounded = false;
        }
    }
    update() {
        if(Input.keyJustPressed("w") && (this.grounded||this.conterjump <= 2)) {
            this.rb.vel.y = -10
            this.conterjump += 1;
        }
        if(Input.keyPressed("a"))
            this.rb.vel.x = -3
        else if(Input.keyPressed("d"))
            this.rb.vel.x = +3
        else this.rb.vel.x = 0
        
    }
}
function setup() {
    let camera  
    size(1500, 700);
    background(51);
    new GameObject(width/2, height/2+10)
    .addComponent(new BoxCollider(50,10))
    .addComponent(new RectRenderer(50,10))
    //spodní strana
    new GameObject(width/2,height+5)
    .addComponent(new BoxCollider(width,10))
    .addComponent(new RectRenderer(width,10))
    //pravá strana
    new GameObject(width+5,height/2)
    .addComponent(new BoxCollider(10,height))
    .addComponent(new RectRenderer(10,height))
    //horní strana
    new GameObject(width/2,-5)
    .addComponent(new BoxCollider(width,10))
    .addComponent(new RectRenderer(width,10))
    //levá strana
    new GameObject(-5,height/2)
    .addComponent(new BoxCollider(10,height))
    .addComponent(new RectRenderer(10,height))
    player = new GameObject(width/2, height/2)
    .addComponent(new RectRenderer(20, 20))
    .addComponent(new DynamicBoxCollider(20, 20))
    .addComponent(new Player());
}

function draw() {
    background(51);
    if(camera == true)
    translate(-player.pos.x + width / 2,-player.pos.y + height / 2)

}
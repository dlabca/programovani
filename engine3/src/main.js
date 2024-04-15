let setjump = 2;
let camera = false;
let player;

class Player extends Component {

    start() {
        this.rb = this.getComponent(Rigidbody);
        this.grounded = false
        this.conterjump = 0;
        this.flywall = false
    }

    update() {
        if (this.flywall) this.rb.vel.y = -575

        if (Input.keyJustPressed("w") && (this.grounded || this.conterjump <= setjump)) {
            this.rb.vel.y = -575
            this.conterjump += 1;
        }
        if (Input.keyPressed("a") && Input.keyPressed("d"))
            this.rb.vel.x = 0
        else if (Input.keyPressed("a"))
            this.rb.vel.x = -174
        else if (Input.keyPressed("d"))
            this.rb.vel.x = +174
        else this.rb.vel.x = 0
    }

    onCollisionEnter(col) {

        if (col.collider.getComponent(Flywall) && col.normal.y == 0) {
            this.flywall = true
            //this.gameObject.pos.x += col.normal.x * 2
        }

        if (col.normal.y == -1) {
            this.grounded = true;
            this.conterjump = 1;
        }
    }

    onCollisionExit(col) {

        if (col.collider.getComponent(Flywall)) this.flywall = false

        if (col.normal.y == -1)
            this.grounded = false;
    }

}
class MovingPlatform extends Component {
    constructor(point1, point2) {
        super();
        this.point1 = point1
        this.point2 = point2

        if (point1 > point2) {
            this.point1 = point2
            this.point2 = point1
        }

        this.dir = 1
    }

    update() {
        if (this.gameObject.pos.x < this.point1) this.dir = 1
        if (this.gameObject.pos.x > this.point2) this.dir = -1

        this.gameObject.pos.x += this.dir * 1
    }
}
class Flywall extends Component { }

function setup() {

    size(1500, 700);

    background(0, 0, 0);
    flywall = new GameObject(780, 425)
        .addComponent(new Rect(10, 150).setColor(0, 255, 0))
        .addComponent(new Rigidbody(0))
        .addComponent(new ShapeRenderer())
        .addComponent(new Flywall())


    new GameObject(width / 8 * 6, height / 8 * 5.5)
        .addComponent(new Rect(50, 10).setColor(0, 0, 255))
        .addComponent(new ShapeRenderer())
        .addComponent(new Rigidbody(0))

    new GameObject(width / 8 * 7, (height / 8) * 6.5)
        .addComponent(new Rect(50, 10).setColor(0, 0, 255))
        .addComponent(new ShapeRenderer())
        .addComponent(new Rigidbody(0))

    new GameObject(width / 2, height / 2)
        .addComponent(new Rect(50, 10).setColor(0, 0, 255))
        .addComponent(new ShapeRenderer())
        .addComponent(new Rigidbody(0))

    new GameObject(810, 505)
        .addComponent(new Rect(50, 10).setColor(0, 0, 255))
        .addComponent(new ShapeRenderer())
        .addComponent(new Rigidbody(0))
        .addComponent(new MovingPlatform(810, 860))

    //spodní strana
    new GameObject(width / 2, height + 5)
        .addComponent(new Rect(width, 10).setColor(100, 100, 200))
        .addComponent(new ShapeRenderer())
        .addComponent(new Rigidbody(0))
    //pravá strana
    new GameObject(width + 5, height / 2)
        .addComponent(new Rect(10, height).setColor(100, 100, 200))
        .addComponent(new ShapeRenderer())
        .addComponent(new Rigidbody(0))
    //horní strana
    new GameObject(width / 2, -5)
        .addComponent(new Rect(width, 10).setColor(100, 100, 200))
        .addComponent(new ShapeRenderer())
        .addComponent(new Rigidbody(0))
    //levá strana
    new GameObject(-5, height / 2)
        .addComponent(new Rect(10, height).setColor(100, 100, 200))
        .addComponent(new ShapeRenderer())
        .addComponent(new Rigidbody(0))
    player = new GameObject(width / 2, height - 10)
        .addComponent(new Rect(20, 20).setColor(255, 0, 0))
        .addComponent(new ShapeRenderer())
        .addComponent(new Rigidbody())
        .addComponent(new Player());


    /*    new GameObject(50, 50)
            .addComponent(new Circle(20).setColor(255, 0, 0))
            .addComponent(new Rigidbody(0))
            .addComponent(new ShapeRenderer())*/
}
function draw() {
    background(51y);
    if (camera == true)
        translate(-player.pos.x + width / 2, -player.pos.y + height / 2);
}

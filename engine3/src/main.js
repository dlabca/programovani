let setjump = 2;
let camera = false;
let player;

class Player extends Component {

    start() {
        this.rb = this.getComponent(DynamicBoxCollider);
        this.grounded = false
        this.conterjump = 0;
        this.flywall = false


        this.rb.onCollisionEnter = (col) => {

            if (col.collider.getComponent(Flywall) && col.hit.normal.y == 0) this.flywall = true

            if (col.hit.normal.y == -1) {
                this.grounded = true;
                this.conterjump = 1;
            }
        }

        this.rb.onCollisionExit = (col) => {

            if (col.collider.getComponent(Flywall)) this.flywall = false

            if (col.hit.normal.y == -1)
                this.grounded = false;
        }
    }

    update() {

        if (this.flywall) this.rb.vel.y = -10

        if (Input.keyJustPressed("w") && (this.grounded || this.conterjump <= setjump)) {
            this.rb.vel.y = -10
            this.conterjump += 1;
        }
        if (Input.keyPressed("a") && Input.keyPressed("d"))
            this.rb.vel.x = 0
        else if (Input.keyPressed("a"))
            this.rb.vel.x = -3
        else if (Input.keyPressed("d"))
            this.rb.vel.x = +3
        else this.rb.vel.x = 0

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
        .addComponent(new RectRenderer(10, 150, () => { fill(0, 255, 0); }))
        .addComponent(new BoxCollider(10, 150))
        .addComponent(new Flywall())

    new GameObject(width / 8 * 6, height / 8 * 5.5)
        .addComponent(new BoxCollider(50, 10))
        .addComponent(new RectRenderer(50, 10, () => { fill(0, 0, 255); }))

    new GameObject(width / 8 * 7, (height / 8) * 6.5)
        .addComponent(new BoxCollider(50, 10))
        .addComponent(new RectRenderer(50, 10, () => { fill(0, 0, 255); }))

    new GameObject(width / 2, height / 2)
        .addComponent(new BoxCollider(50, 10))
        .addComponent(new RectRenderer(50, 10, () => { fill(0, 0, 255); }))

    new GameObject(810, 505)
        .addComponent(new RectRenderer(50, 10, () => { fill(0, 0, 255); }))
        .addComponent(new BoxCollider(50, 10))
        .addComponent(new MovingPlatform(810, 860))

        //spodní strana
    new GameObject(width / 2, height + 5)
        .addComponent(new BoxCollider(width, 10))
        .addComponent(new RectRenderer(width, 10))
    //pravá strana
    new GameObject(width + 5, height / 2)
        .addComponent(new BoxCollider(10, height))
        .addComponent(new RectRenderer(10, height))
    //horní strana
    new GameObject(width / 2, -5)
        .addComponent(new BoxCollider(width, 10))
        .addComponent(new RectRenderer(width, 10))
    //levá strana
    new GameObject(-5, height / 2)
        .addComponent(new BoxCollider(10, height))
        .addComponent(new RectRenderer(10, height))
    player = new GameObject(width / 2, height - 10)
        .addComponent(new RectRenderer(20, 20, () => { fill(255, 0, 0); }))
        .addComponent(new DynamicBoxCollider(20, 20))
        .addComponent(new Player());

}

function draw() {
    background(51);
    if (camera == true)
        translate(-player.pos.x + width / 2, -player.pos.y + height / 2)

}
let rb;

Rigidbody.prototype.draw = function() {
    fill(255, 0, 0);
    circle(this.pos.x, this.pos.y, 20);
}

Rigidbody.prototype.update = function() {
    this.pos.add(this.vel);
}

function setup() {
    createCanvas(400, 400);
    rb = new Rigidbody(width / 2, 0);
}

function draw() {
    background(255);
    rb.applyForces();
    rb.update();
    rb.draw();
}
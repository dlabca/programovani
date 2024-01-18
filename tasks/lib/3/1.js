let rb;

Rigidbody.prototype.draw = function() {
    fill(255, 0, 0);
    circle(this.pos.x, this.pos.y, 20);
}

function setup() {
    createCanvas(400, 400);
    rb = new Rigidbody(width / 2, height / 2);
}

function draw() {
    rb.draw();
}
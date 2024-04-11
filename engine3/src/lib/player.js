"use strict";
class Player extends Component {
    speed = 180;
    jumpForce = 600;
    maxJumps = 2;
    jumps = this.maxJumps;
    elevator = false;
    start() {
        this.rb = this.getComponent(Rigidbody);
        this.grounded = false;
    }
    update() {
        if (this.elevator)
            this.rb.vel.y = -this.jumpForce;
        if (Input.keyJustPressed('w') && this.jumps > 1) {
            this.rb.vel.y = -this.jumpForce;
            this.jumps--;
        }
        if (this.grounded)
            this.jumps = 2;
    }
    fixedUpdate() {
        if (Input.keyPressed('a'))
            this.rb.applyForce(-this.speed, 0);
        if (Input.keyPressed('d'))
            this.rb.applyForce(this.speed, 0);
    }
    onCollisionEnter(col) {
        if (col.collider.gameObject == elevator && col.normal.y != -1)
            this.elevator = true;
        if (col.normal.y == -1 && col.collider.getComponent(Enemy)) {
            col.collider.gameObject.destroy();
            this.rb.vel.y = -this.jumpForce;
            return;
        }
        if (col.normal.y == -1 && col.normal.x == 0)
            this.grounded = true;
        console.log("Enter:", col);
    }
    onCollisionExit(col) {
        if (col.collider.gameObject == elevator)
            this.elevator = false;
        if (col.normal.y == -1 && col.normal.x == 0)
            this.grounded = false;
        console.log("Exit", this.grounded);
    }
}

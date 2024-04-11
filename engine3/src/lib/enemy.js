"use strict";
class Enemy extends Component {
    speed = 120;
    start() {
        this.rb = this.getComponent(DynamicBoxCollider);
        this.rb.onCollisionEnter = col => {
            if (col.hit.normal.y == 0)
                this.speed *= -1;
        };
    }
    update() {
        this.rb.vel.x = this.speed;
    }
}

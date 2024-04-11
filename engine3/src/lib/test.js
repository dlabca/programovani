"use strict";
class TestComponent extends Component {
    onCollisionEnter(col) {
        this.getComponent(Rect).setColor(255, 0, 0);
    }
}
class TestComponent1 extends Component {
    // Predtim
    start() {
        let rb = this.getComponent(Rigidbody);
        rb.onCollisionEnter = (col) => {
        };
    }
    // Ted
    onCollisionEnter(col) {
    }
}

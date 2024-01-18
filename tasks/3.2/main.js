
// Na třídě Rigidbody je teď funkce update
// Doplňte ji tak, aby se Rigidbody posouvalo dolů

class Rigidbody {
    constructor(x, y) {
        this.pos = createVector(x, y);
    }

    update() {
        this.pos.y += 1;
    }
}
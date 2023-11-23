class Input {
    static keys = {};

    static keyPressed(key) {
        return key in Input.keys && Input.keys[key];
    }
}

function keyPressed() {
    Input.keys[key] = true;
}

function keyReleased() {
    Input.keys[key] = false;
}
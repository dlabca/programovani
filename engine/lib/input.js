class Input {
    static keys = {};
    static lastKeys = {};

    static keyPressed(key) {
        return key in Input.keys && Input.keys[key];
    }

    static keyJustPressed(key) {
        return Input.keys[key] && !Input.lastKeys[key];
    }

    static keyJustReleased(key) {
        return !Input.keys[key] && Input.lastKeys[key];
    }

    static update() {
        Object.assign(Input.lastKeys, Input.keys);
    }
}

function keyPressed() {
    Input.keys[key] = true;
}

function keyReleased() {
    Input.keys[key] = false;
}
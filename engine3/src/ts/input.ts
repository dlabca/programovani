class Input {
    static keys: { [key: string]: boolean } = {};
    static lastKeys: { [key: string]: boolean } = {};

    static keyPressed(key: string) {
        return key in Input.keys && Input.keys[key];
    }

    static keyJustPressed(key: string) {
        return Input.keys[key] && !Input.lastKeys[key];
    }

    static keyJustReleased(key: string) {
        return !Input.keys[key] && Input.lastKeys[key];
    }

    static update() {
        Object.assign(Input.lastKeys, Input.keys);
    }
}
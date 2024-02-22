declare class Input {
    static keys: {
        [key: string]: boolean;
    };
    static lastKeys: {
        [key: string]: boolean;
    };
    static keyPressed(key: string): boolean;
    static keyJustPressed(key: string): boolean;
    static keyJustReleased(key: string): boolean;
    static update(): void;
}

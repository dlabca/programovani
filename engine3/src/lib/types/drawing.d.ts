declare class Color {
    r: number;
    g: number;
    b: number;
    constructor(...args: [r: number, g: number, b: number] | [c: Color] | [gray: number]);
    set(...args: [r: number, g: number, b: number] | [c: Color] | [gray: number]): void;
    toString(): string;
}
declare class Matrix {
    a: number;
    b: number;
    c: number;
    d: number;
    e: number;
    f: number;
    constructor(a: number, b: number, c: number, d: number, e: number, f: number);
    set(m: Matrix): void;
    clear(): void;
    static identity(): Matrix;
    static translation(x: number, y: number): Matrix;
    static mult(m1: Matrix, m2: Matrix): Matrix;
}
declare class DrawSettings {
    fillColor: Color | null;
    strokeColor: Color | null;
    transform: Matrix;
}
declare class Draw {
    static ctx: CanvasRenderingContext2D;
    static settings: DrawSettings;
    static stack: Array<DrawSettings>;
    static applyStack(): void;
    private static transform;
}
declare function size(w: number, h: number): void;
declare function fill(...args: [r: number, g: number, b: number] | [c: Color] | [gray: number]): void;
declare function noFill(): void;
declare function stroke(...args: [r: number, g: number, b: number] | [c: Color] | [gray: number]): void;
declare function noStroke(): void;
declare function translate(x: number, y: number): void;
declare function rotate(a: number): void;
declare function scale(...args: [x: number, y: number] | [s: number]): void;
declare function push(): void;
declare function pop(): void;
declare function background(...args: [r: number, g: number, b: number] | [c: Color] | [gray: number]): void;
declare function rect(x: number, y: number, w: number, h: number): void;

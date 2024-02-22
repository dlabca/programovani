declare class Vector {
    x: number;
    y: number;
    constructor(...args: [x: number, y: number] | [v: Vector] | []);
    set(...args: [x: number, y: number] | [v: Vector] | []): Vector;
    mag(): number;
    add(...args: [x: number, y: number] | [v: Vector]): Vector;
    sub(...args: [x: number, y: number] | [v: Vector]): Vector;
    mult(...args: [n: number] | [x: number, y: number] | [v: Vector]): Vector;
    static add(v1: Vector, v2: Vector): Vector;
    static sub(v1: Vector, v2: Vector): Vector;
    static mult(...args: [v: Vector, n: number] | [n: number, v: Vector] | [v1: Vector, v2: Vector]): Vector;
    static lerp(a: Vector, b: Vector, t: number): Vector;
    static up(): Vector;
    static right(): Vector;
    static down(): Vector;
    static left(): Vector;
}
declare function createVector(...args: [x: number, y: number] | []): Vector;

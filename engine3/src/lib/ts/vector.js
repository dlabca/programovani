"use strict";
class Vector {
    x = 0;
    y = 0;
    constructor(...args) {
        this.set(...args);
    }
    set(...args) {
        this.x = 0;
        this.y = 0;
        if (args[0] instanceof Vector) {
            this.x = args[0].x;
            this.y = args[0].y;
        }
        else if (typeof args[0] == "number" && typeof args[1] == "number") {
            this.x = args[0];
            this.y = args[1];
        }
        return this;
    }
    mag() {
        return Math.sqrt(this.x * this.x + this.y * this.y);
    }
    add(...args) {
        if (args[0] instanceof Vector) {
            this.x += args[0].x;
            this.y += args[0].y;
        }
        else if (typeof args[1] == "number") {
            this.x += args[0];
            this.y += args[1];
        }
        else
            throw "Invalid arguments";
        return this;
    }
    sub(...args) {
        if (args[0] instanceof Vector) {
            this.x -= args[0].x;
            this.y -= args[0].y;
        }
        else if (typeof args[1] == "number") {
            this.x -= args[0];
            this.y -= args[1];
        }
        else
            throw "Invalid arguments";
        return this;
    }
    mult(...args) {
        if (args[0] instanceof Vector) {
            this.x *= args[0].x;
            this.y *= args[0].y;
        }
        else if (args[1] == undefined) {
            this.x *= args[0];
            this.y *= args[0];
        }
        else {
            this.x *= args[0];
            this.y *= args[1];
        }
        return this;
    }
    static add(v1, v2) {
        return new Vector(v1.x + v2.x, v1.y + v2.y);
    }
    static sub(v1, v2) {
        return new Vector(v1.x - v2.x, v1.y - v2.y);
    }
    static mult(...args) {
        if (args[0] instanceof Vector && args[1] instanceof Vector)
            return new Vector(args[0].x * args[1].x, args[0].y * args[1].y);
        else if (args[0] instanceof Vector && typeof args[1] == "number")
            return new Vector(args[0].x * args[1], args[0].y * args[1]);
        else if (typeof args[0] == "number" && args[1] instanceof Vector)
            return new Vector(args[0] * args[1].x, args[0] * args[1].y);
        throw "Invalid arguments";
    }
    static lerp(a, b, t) {
        return Vector.add(Vector.mult(a, 1 - t), Vector.mult(b, t));
    }
    static dot(a, b) {
        return a.x * b.x + a.y * b.y;
    }
    static cross(a, b) {
        return a.x * b.y - a.y * b.x;
    }
    static dist(a, b) {
        return this.sub(b, a).mag();
    }
    static up() { return new Vector(0, -1); }
    static right() { return new Vector(1, 0); }
    static down() { return new Vector(0, 1); }
    static left() { return new Vector(-1, 0); }
}
function createVector(...args) {
    return new Vector(...args);
}

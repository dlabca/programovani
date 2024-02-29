"use strict";
class Color {
    r = 0;
    g = 0;
    b = 0;
    constructor(...args) {
        this.set(...args);
    }
    set(...args) {
        if (args[0] instanceof Color) {
            this.set(args[0].r, args[0].g, args[0].b);
        }
        else if (typeof args[1] == "number") {
            this.r = args[0];
            this.g = args[1];
            this.b = args[2];
        }
        else {
            this.set(args[0], args[0], args[0]);
        }
    }
    toString() {
        const clamp = (value) => Math.min(255, Math.max(0, value));
        this.r = clamp(this.r);
        this.g = clamp(this.g);
        this.b = clamp(this.b);
        // Convert each component to its hex representation and concatenate them
        const hexR = (this.r < 16 ? '0' : '') + this.r.toString(16);
        const hexG = (this.g < 16 ? '0' : '') + this.g.toString(16);
        const hexB = (this.b < 16 ? '0' : '') + this.b.toString(16);
        // Concatenate the hex values and return
        return `#${hexR}${hexG}${hexB}`;
    }
}
class Matrix {
    a;
    b;
    c;
    d;
    e;
    f;
    constructor(a, b, c, d, e, f) {
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
        this.e = e;
        this.f = f;
    }
    set(m) {
        this.a = m.a;
        this.b = m.b;
        this.c = m.c;
        this.d = m.d;
        this.e = m.e;
        this.f = m.f;
    }
    clear() {
        this.a = 1;
        this.c = 0;
        this.e = 0;
        this.b = 0;
        this.d = 1;
        this.f = 0;
    }
    static identity() {
        return new Matrix(1, 0, 0, 1, 0, 0);
    }
    static translation(x, y) {
        return new Matrix(1, 0, 0, 1, x, y);
    }
    static mult(m1, m2) {
        return new Matrix(m1.a * m2.a + m1.c * m2.b, m1.b * m2.a + m1.d * m2.b, m1.a * m2.c + m1.c * m2.d, m1.b * m2.c + m1.d * m2.d, m1.a * m2.e + m1.c * m2.f + m1.e, m1.b * m2.e + m1.d * m2.f + m1.f);
    }
}
class DrawSettings {
    fillColor = new Color(255);
    strokeColor = new Color(0);
    transform = Matrix.identity();
}
class Draw {
    static ctx;
    static settings = new DrawSettings();
    static stack = [];
    static applyStack() {
        Draw.ctx.resetTransform();
        for (const setting of Draw.stack) {
            Draw.transform(setting.transform);
        }
        Draw.transform(Draw.settings.transform);
        if (Draw.settings.fillColor)
            Draw.ctx.fillStyle = Draw.settings.fillColor.toString();
        if (Draw.settings.strokeColor)
            Draw.ctx.strokeStyle = Draw.settings.strokeColor.toString();
    }
    static transform(matrix) {
        Draw.ctx.transform(matrix.a, matrix.b, matrix.c, matrix.d, matrix.e, matrix.f);
    }
}
//#region Global drawing functions
//#region Settings
function size(w, h) {
    Draw.ctx.canvas.width = width = w;
    Draw.ctx.canvas.height = height = h;
}
function fill(...args) {
    if (Draw.settings.fillColor == null)
        Draw.settings.fillColor = new Color(0, 0, 0);
    Draw.settings.fillColor.set(...args);
}
function noFill() {
    Draw.settings.fillColor = null;
}
function stroke(...args) {
    if (Draw.settings.strokeColor == null)
        Draw.settings.strokeColor = new Color(0, 0, 0);
    Draw.settings.strokeColor.set(...args);
}
function noStroke() {
    Draw.settings.strokeColor = null;
}
function translate(x, y) {
    Draw.settings.transform.set(Matrix.mult(Matrix.translation(x, y), Draw.settings.transform));
}
function rotate(a) {
    // TODO
    throw "Not implemented";
}
function scale(...args) {
    // TODO
    // if (args[1])
    //     Draw.ctx.scale(args[0], args[1]);
    // else
    //     Draw.ctx.scale(args[0], args[0]);
    throw "Not implemented";
}
function push() {
    Draw.stack.push(Draw.settings);
    Draw.settings = new DrawSettings();
}
function pop() {
    let settings = Draw.stack.pop();
    if (settings)
        Draw.settings = settings;
}
//#endregion
//#region Drawing
function background(...args) {
    let col = new Color(...args);
    Draw.ctx.fillStyle = col.toString();
    let m = Draw.ctx.getTransform().inverse();
    Draw.ctx.resetTransform();
    Draw.ctx.fillRect(0, 0, width, height);
    Draw.ctx.setTransform(m);
}
function rect(x, y, w, h) {
    Draw.applyStack();
    if (Draw.settings.fillColor != null) {
        Draw.ctx.fillRect(x, y, w, h);
    }
    if (Draw.settings.strokeColor != null) {
        Draw.ctx.strokeRect(x, y, w, h);
    }
}
//#endregion
//#endregion

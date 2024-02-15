let objects = [];

class Engine {

    static start() {
        for (const gameObject of objects) {
            gameObject.start();
        }
    }

    static update() {
        for (const gameObject of objects) {
            gameObject.update();
        }
        Input.update();
    }
}

class GameObject {
    constructor(x, y) {
        objects.push(this);

        this.pos = createVector(x, y);
        this.components = [];
    }

    start() {
        for (const component of this.components) {
            component.start();
        }
    }

    update() {
        for (const component of this.components) {
            component.update();
        }
    }

    addComponent(component) {
        this.components.push(component);
        component.gameObject = this;
        return this;
    }

    getComponent(type) {
        for (const component of this.components) {
            if (component instanceof type) return component;
        }
    }

    destroy() {
        objects.splice(objects.indexOf(this), 1);
    }
}

class Component {
    constructor() {
        this.gameObject = undefined;
    }

    getComponent(type) { return this.gameObject.getComponent(type); }

    start() {}

    update() {}
}

class RectRenderer extends Component {
    constructor(w, h, drawSettings = () => fill(255)) {
        super();
        this.size = createVector(w, h);
        this.settings = drawSettings;
    }

    update() {
        push();
        this.settings();
        rect(this.gameObject.pos.x - this.size.x / 2, this.gameObject.pos.y - this.size.y / 2, this.size.x, this.size.y);
        pop();
    }
}

class BoxCollider extends Component {
    constructor(w, h) {
        super();
        this.size = createVector(w, h);
    }
}

const gravity = 0.5;
class DynamicBoxCollider extends BoxCollider {
    constructor(w, h) {
        super(w, h);

        this.vel = createVector();
    }

    update() {
        this.applyForce(0, gravity);

        this.resolveCollisions();
        
        this.gameObject.pos.add(this.vel);
    }

    applyForce(x, y) {
        if (x instanceof p5.Vector)
            this.vel.add(x);
        else
            this.vel.add(x, y);
    }

    resolveCollisions() {
        if (this.vel.x == 0 && this.vel.y == 0) return;

        let collisions = [];

        for (const gameObject of objects) {
            if (gameObject == this) continue;
            for (const collider of gameObject.components) {
                if (!(collider instanceof BoxCollider)) continue;

                let collision = this.detectCollision(collider);
                if (collision)
                    collisions.push(collision);
            }
        }

        collisions.sort((a, b) => a.t - b.t);

        for (const collision of collisions) {
            let hit = this.detectCollision(collision.collider);
            if (hit) {
                this.vel.add(p5.Vector.mult(hit.normal, createVector(abs(this.vel.x), abs(this.vel.y)).mult(1 - hit.t)));
            }
        }
    }

    detectCollision(other) {
        if (this.vel.x == 0 && this.vel.y == 0) return undefined;

        let rect = {
            x: other.gameObject.pos.x - other.size.x / 2,
            y: other.gameObject.pos.y - other.size.y / 2,
            w: other.size.x,
            h: other.size.y
        };

        let pos = createVector(rect.x - this.size.x / 2., rect.y - this.size.y / 2.);
        let size = createVector(rect.w + this.size.x, rect.h + this.size.y);

        let hit = Physics.rayVsRect(this.gameObject.pos, this.vel, pos, size);
        if (!hit || hit.t < 0 || hit.t >= 1) return undefined;
        
        return { collider: other, t: hit.t, normal: hit.normal };
    }
}

class Physics {
    static rayVsRect(origin, direction, pos, size) {
        let normal = createVector();
        let point = createVector();
    
        let invDir = createVector(1. / direction.x, 1. / direction.y);
    
        let tNear = p5.Vector.sub(pos, origin); tNear.x *= invDir.x; tNear.y *= invDir.y;
        let tFar = p5.Vector.sub(p5.Vector.add(pos, size), origin); tFar.x *= invDir.x; tFar.y *= invDir.y;
    
        if (isNaN(tFar.y) || isNaN(tFar.x)) return undefined;
        if (isNaN(tNear.y) || isNaN(tNear.x)) return undefined;
    
        if (tNear.x > tFar.x) { let tmp = tNear.x; tNear.x = tFar.x; tFar.x = tmp; }
        if (tNear.y > tFar.y) { let tmp = tNear.y; tNear.y = tFar.y; tFar.y = tmp; }
    
        if (tNear.x > tFar.y || tNear.y > tFar.y) return undefined;
    
        let tHitNear = max(tNear.x, tNear.y);
        let tHitFar = min(tFar.x, tFar.y);
    
        if (tHitFar < 0) return undefined;
    
        point.add(origin); point.add(p5.Vector.mult(direction, tHitNear));
    
        if (tNear.x > tNear.y) {
            if (invDir.x < 0) normal.set(1, 0);
            else normal.set(-1, 0);
        }
        else if (tNear.x < tNear.y) {
            if (invDir.y < 0) normal.set(0, 1);
            else normal.set(0, -1);
        }
    
        return {
            point: point,
            normal: normal,
            t: tHitNear
        };
    }
}
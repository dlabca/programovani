interface RayHit {
    point: Vector;
    t: number;
    normal: Vector;
    distance: number;
}

interface BoxCollision {
    collider: BoxCollider,
    hit: RayHit
}

abstract class PhysicsComponent extends Component {
    constructor() {
        super();
        Physics.physicsComponents.push(this);
    }

    abstract intersectRay(origin: Vector, direction: Vector): RayHit | null;

    destroy(): void {
        Physics.physicsComponents.splice(Physics.physicsComponents.indexOf(this), 1);
    }
}

class BoxCollider extends PhysicsComponent {

    size: Vector;

    constructor(w: number, h: number) {
        super();
        this.size = createVector(w, h);
    }

    intersectRay(origin: Vector, direction: Vector): RayHit | null {
        return Physics.rayVsRect(origin, direction, this.gameObject.pos, this.size);
    }
}

class DynamicBoxCollider extends BoxCollider {

    vel: Vector;
    applyGravity: boolean = true;

    private collisions: Array<BoxCollision> = [];
    onCollisionEnter: (collision: BoxCollision) => void = () => {};
    onCollisionExit: (collision: BoxCollision) => void = () => {};

    constructor(w: number, h: number) {
        super(w, h);

        this.vel = createVector();
    }

    update() {
        if (this.applyGravity)
            this.applyForce(Physics.gravity);

        this.resolveCollisions();
        
        this.gameObject.pos.add(Vector.mult(this.vel, deltaTime));
    }

    applyForce(...args: [x: number, y: number] | [v: Vector]) {
        this.vel.add(...args);
    }

    resolveCollisions() {
        if (this.vel.x == 0 && this.vel.y == 0) return;

        let collisions: Array<BoxCollision> = [];

        for (const collider of Physics.physicsComponents) {
            if (collider == this) continue;

            if (!(collider instanceof BoxCollider)) continue;

            let collision = this.detectCollision(collider);
            if (collision)
                collisions.push(collision);
        }

        collisions.sort((a, b) => a.hit.t - b.hit.t);

        for (const collision of collisions) {
            let hit = this.detectCollision(collision.collider)?.hit;
            if (hit) {
                this.vel.add(Vector.mult(hit.normal, createVector(Math.abs(this.vel.x), Math.abs(this.vel.y)).mult(1 - hit.t)));
                if (!this.collisions.find(col => col.collider == collision.collider)) {
                    if (this.onCollisionEnter)
                        this.onCollisionEnter(collision);
                    this.collisions.push(collision);
                }
            }
        }

        for (let i = this.collisions.length - 1; i >= 0; i--) {
            const collision = this.collisions[i];
            if (!collisions.find(col => col.collider == collision.collider)) {
                if (this.onCollisionExit)
                    this.onCollisionExit(collision);
                this.collisions.splice(i, 1);
            }
        }
    }

    detectCollision(other: BoxCollider): BoxCollision | null {
        if (this.vel.x == 0 && this.vel.y == 0) return null;

        let rect = {
            x: other.gameObject.pos.x,
            y: other.gameObject.pos.y,
            w: other.size.x,
            h: other.size.y
        };

        let pos = createVector(rect.x, rect.y);
        let size = createVector(rect.w + this.size.x, rect.h + this.size.y);

        let hit = Physics.rayVsRect(this.gameObject.pos, Vector.mult(this.vel, deltaTime), pos, size);
        if (!hit || hit.t < 0 || hit.t >= 1) return null;
        
        return { collider: other, hit: hit };
    }
}

class Physics {

    static gravity: Vector = createVector(0, 0.5);
    static physicsComponents: Array<PhysicsComponent> = [];

    // pos: center of the rect
    static rayVsRect(origin: Vector, direction: Vector, pos: Vector, size: Vector): RayHit | null {
        let normal = createVector();
        let point = createVector();
    
        let invDir = createVector(1. / direction.x, 1. / direction.y);
    
        let corner = Vector.sub(pos, Vector.mult(size, 0.5));

        let tNear = Vector.sub(corner, origin); tNear.x *= invDir.x; tNear.y *= invDir.y;
        let tFar = Vector.sub(Vector.add(corner, size), origin); tFar.x *= invDir.x; tFar.y *= invDir.y;
    
        if (isNaN(tFar.y) || isNaN(tFar.x)) return null;
        if (isNaN(tNear.y) || isNaN(tNear.x)) return null;
    
        if (tNear.x > tFar.x) { let tmp = tNear.x; tNear.x = tFar.x; tFar.x = tmp; }
        if (tNear.y > tFar.y) { let tmp = tNear.y; tNear.y = tFar.y; tFar.y = tmp; }
    
        if (tNear.x > tFar.y || tNear.y > tFar.y) return null;
    
        let tHitNear = Math.max(tNear.x, tNear.y);
        let tHitFar = Math.min(tFar.x, tFar.y);
    
        if (tHitFar < 0) return null;
    
        point.add(origin); point.add(Vector.mult(direction, tHitNear));
    
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
            t: tHitNear,
            distance: tHitNear * direction.mag()
        };
    }

    static raycast(origin: Vector, direction: Vector, range: number = Infinity): RayHit | null {
        let out: RayHit | null = null;
        let minDist: number = Infinity;

        for (const collider of Physics.physicsComponents) {
            let hit = collider.intersectRay(origin, direction);
            if (hit && hit.distance < minDist) out = hit;
        }

        if (out && out.distance >= 0 && out.distance <= range) return out;

        return null;
    }
}
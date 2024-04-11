"use strict";
class CollisionManifold {
    rigiA;
    rigiB;
    /**
     * @description Points from rigiB to rigiA
     */
    normal;
    point;
    depth;
    constructor(rigiA, rigiB, normal, point, depth) {
        this.rigiA = rigiA;
        this.rigiB = rigiB;
        this.normal = new Vector(normal);
        this.point = new Vector(point);
        this.depth = depth;
    }
    resolveCollision() {
        let relativeVelocity = Vector.sub(this.rigiB.vel, this.rigiA.vel);
        let relativeVelocityAlongNormal = Vector.dot(relativeVelocity, this.normal);
        if (relativeVelocityAlongNormal > 0)
            return;
        let e = Math.min(this.rigiA.bounciness, this.rigiB.bounciness);
        if (!isFinite(e))
            e = 0;
        let invMassSum = this.rigiA.invMass + this.rigiB.invMass;
        let j = -(1 + e) * relativeVelocityAlongNormal / invMassSum;
        let impulse = Vector.mult(this.normal, j);
        let impulseA = Vector.mult(impulse, -this.rigiA.invMass);
        let impulseB = Vector.mult(impulse, this.rigiB.invMass);
        this.rigiA.vel.add(impulseA);
        this.rigiB.vel.add(impulseB);
        this.rigiA.currentCollisions.set(this.rigiB, new Collision(this.rigiB, Vector.mult(this.normal, -1), this.depth));
        this.rigiB.currentCollisions.set(this.rigiA, new Collision(this.rigiA, Vector.mult(this.normal, 1), this.depth));
        // Friction
        let penetrationToCentroidA = Vector.sub(this.point, this.rigiA.gameObject.pos);
        let penetrationToCentroidB = Vector.sub(this.point, this.rigiB.gameObject.pos);
        let velocityInNormalDirection = Vector.mult(this.normal, relativeVelocityAlongNormal);
        let tangent = Vector.sub(velocityInNormalDirection, relativeVelocity);
        let minFriction = Math.min(this.rigiA.friction, this.rigiB.friction);
        if (tangent.x > 0.0001 && tangent.y > 0.0001) {
            tangent.mult(1 / tangent.mag());
        }
        let pToCentroidCrossTangentA = Vector.cross(penetrationToCentroidA, tangent);
        let pToCentroidCrossTangentB = Vector.cross(penetrationToCentroidB, tangent);
        let crossSumTangent = pToCentroidCrossTangentA * pToCentroidCrossTangentA * this.rigiA.invInertia +
            pToCentroidCrossTangentB * pToCentroidCrossTangentB * this.rigiB.invInertia;
        let frictionalImpulse = -(1 + e) * Vector.dot(relativeVelocity, tangent) * minFriction;
        frictionalImpulse /= invMassSum + crossSumTangent;
        if (frictionalImpulse > j)
            frictionalImpulse = j;
        let frictionalImpulseVector = Vector.mult(tangent, frictionalImpulse);
        this.rigiA.vel.sub(Vector.mult(frictionalImpulseVector, this.rigiA.invMass));
        this.rigiB.vel.add(Vector.mult(frictionalImpulseVector, this.rigiB.invMass));
    }
    positionalCorrection() {
        let correctionPercentage = 0.2;
        let amountToCorrect = this.depth / (this.rigiA.invMass + this.rigiB.invMass) * correctionPercentage;
        let correctionVector = Vector.mult(this.normal, amountToCorrect);
        let rigiACorrection = Vector.mult(correctionVector, -this.rigiA.invMass);
        let rigiBCorrection = Vector.mult(correctionVector, this.rigiB.invMass);
        if (!this.rigiA.isKinematic)
            this.rigiA.gameObject.pos.add(rigiACorrection);
        if (!this.rigiB.isKinematic)
            this.rigiB.gameObject.pos.add(rigiBCorrection);
    }
}
class Collision {
    collider;
    normal;
    depth;
    constructor(collider, normal, depth) {
        this.collider = collider;
        this.normal = normal;
        this.depth = depth;
    }
}
class Rigidbody extends Component {
    vel = new Vector();
    forceAccumulator = new Vector();
    applyGravity = true;
    mass = 1;
    invMass;
    inertia = 0;
    invInertia = 0;
    bounciness = 0;
    friction = 0.1;
    isKinematic = false;
    shape;
    collisions = new Map();
    currentCollisions = new Map();
    constructor(mass = 1, bounciness = 0, friction = 0.1) {
        super();
        this.mass = mass;
        this.bounciness = bounciness;
        this.friction = friction;
        if (this.mass > 0.0001)
            this.invMass = 1 / this.mass;
        else {
            this.invMass = 0;
            this.isKinematic = true;
        }
        Physics.bodies.push(this);
    }
    start() {
        let shape = this.getComponent(Shape);
        if (!shape) {
            console.warn("An object with a Rigidbody component must also have a shape component");
            return;
        }
        this.shape = shape;
        this.inertia = this.shape.calculateInertia(this.mass);
        if (this.inertia > 0.0001)
            this.invInertia = 1 / this.inertia;
        else
            this.invInertia = 0;
    }
    physicsUpdate() {
        if (this.applyGravity)
            this.applyForce(Physics.gravity);
        let acc = Vector.mult(this.forceAccumulator, this.invMass);
        this.vel.add(Vector.mult(acc, fixedDeltaTime));
        this.gameObject.pos.add(Vector.mult(this.vel, fixedDeltaTime));
        this.vel.mult(0.9999);
        this.forceAccumulator.mult(0);
        this.processCollisions();
    }
    processCollisions() {
        // onCollisionEnter events
        for (const [key, val] of this.currentCollisions) {
            if (!this.collisions.has(key)) {
                this.collisions.set(key, val);
                this.gameObject.onCollisionEnter(val);
            }
        }
        // onCollisionExit events
        let toRemove = [];
        for (const [key, val] of this.collisions) {
            if (!this.currentCollisions.has(key)) {
                toRemove.push(key);
                this.gameObject.onCollisionExit(val);
            }
        }
        // Removing exitted collisions
        for (const key of toRemove) {
            this.collisions.delete(key);
        }
        // Clearing current collisions
        this.currentCollisions.clear();
    }
    applyForce(...args) {
        this.forceAccumulator.add(...args);
    }
    destroy() {
        Physics.bodies.splice(Physics.bodies.indexOf(this), 1);
    }
}
class PhysicsComponent extends Component {
    collisions = [];
    constructor() {
        super();
        Physics.physicsComponents.push(this);
    }
    destroy() {
        Physics.physicsComponents.splice(Physics.physicsComponents.indexOf(this), 1);
    }
}
class BoxCollider extends PhysicsComponent {
    size;
    constructor(w, h) {
        super();
        this.size = createVector(w, h);
    }
    intersectRay(origin, direction) {
        return Physics.rayVsRect(origin, direction, this.gameObject.pos, this.size);
    }
}
class DynamicBoxCollider extends BoxCollider {
    vel;
    applyGravity = true;
    constructor(w, h) {
        super(w, h);
        this.vel = createVector();
    }
    update() {
        if (this.applyGravity)
            this.applyForce(Vector.mult(Physics.gravity, 1 / 60));
        this.resolveCollisions();
        this.gameObject.pos.add(Vector.mult(this.vel, deltaTime));
    }
    applyForce(...args) {
        this.vel.add(...args);
    }
    resolveCollisions() {
        if (this.vel.x == 0 && this.vel.y == 0)
            return;
        // Detect collisions
        let collisions = [];
        for (const collider of Physics.physicsComponents) {
            if (collider == this)
                continue;
            if (!(collider instanceof BoxCollider))
                continue;
            let collision = this.detectCollision(collider);
            if (collision)
                collisions.push(collision);
        }
        // Resolve collisions (in correct order)
        collisions.sort((a, b) => a.hit.t - b.hit.t);
        for (const collision of collisions) {
            let hit = this.detectCollision(collision.collider)?.hit;
            if (hit) {
                this.vel.add(Vector.mult(hit.normal, createVector(Math.abs(this.vel.x), Math.abs(this.vel.y)).mult(1 - hit.t)));
                if (!this.collisions.find(col => col.collider == collision.collider)) {
                    // if (this.onCollisionEnter)
                    //     this.onCollisionEnter(collision);
                    this.collisions.push(collision);
                }
            }
        }
        // Call collision exit events
        for (let i = this.collisions.length - 1; i >= 0; i--) {
            const collision = this.collisions[i];
            if (!collisions.find(col => col.collider == collision.collider)) {
                // if (this.onCollisionExit)
                //     this.onCollisionExit(collision);
                this.collisions.splice(i, 1);
            }
        }
    }
    detectCollision(other) {
        if (this.vel.x == 0 && this.vel.y == 0)
            return null;
        let rect = {
            x: other.gameObject.pos.x,
            y: other.gameObject.pos.y,
            w: other.size.x,
            h: other.size.y
        };
        let pos = createVector(rect.x, rect.y);
        let size = createVector(rect.w + this.size.x, rect.h + this.size.y);
        let hit = Physics.rayVsRect(this.gameObject.pos, Vector.mult(this.vel, deltaTime), pos, size);
        if (!hit || hit.t < 0 || hit.t >= 1)
            return null;
        return { collider: other, hit: hit };
    }
}
class Physics {
    static gravity = createVector(0, 1800);
    static bodies = [];
    static physicsComponents = [];
    static update() {
        for (const rigiA of this.bodies) {
            for (const rigiB of this.bodies) {
                if (rigiA == rigiB || (!rigiA.enabled || !rigiB.enabled))
                    continue;
                let col = this.checkCollision(rigiA, rigiB);
                if (!col)
                    continue;
                col.resolveCollision();
                col.positionalCorrection();
            }
        }
        for (const rb of this.bodies) {
            rb.physicsUpdate();
        }
    }
    static checkCollision(rigiA, rigiB) {
        if (rigiA.isKinematic && rigiB.isKinematic)
            return;
        if (rigiA.shape instanceof Rect && rigiB.shape instanceof Rect) {
            return this.rectVsRect(rigiA, rigiB);
        }
        else if (rigiA.shape instanceof Circle && rigiB.shape instanceof Rect) {
            return this.circleVsRect(rigiA, rigiB);
        }
        else if (rigiA.shape instanceof Rect && rigiB.shape instanceof Circle) {
        }
        else if (rigiA.shape instanceof Circle && rigiB.shape instanceof Circle) {
            return this.circleVsCircle(rigiA, rigiB);
        }
        else {
            console.warn(`Collision of invalid shapes: ${rigiA.shape}, ${rigiB.shape}`);
        }
        return;
    }
    static dynamicVsDynamicRect(rigiA, rigiB) {
        if (rigiA.vel.x == 0 && rigiA.vel.y == 0)
            return;
        if (!(rigiA.shape instanceof Rect) || !(rigiB.shape instanceof Rect))
            return;
        let pos = new Vector(rigiB.gameObject.pos);
        let size = Vector.add(rigiA.shape.size, rigiB.shape.size);
        let hit = Physics.rayVsRect(rigiA.gameObject.pos, Vector.mult(rigiA.vel, deltaTime), pos, size);
        if (!hit || hit.t < 0 || hit.t >= 1)
            return;
        return new CollisionManifold(rigiA, rigiB, hit.normal, hit.point, hit.distance);
    }
    static rectVsRect(rigiA, rigiB) {
        let rectA = rigiA.shape;
        let rectB = rigiB.shape;
        let minA = Vector.sub(rigiA.gameObject.pos, Vector.mult(rectA.size, 0.5));
        let maxA = Vector.add(rigiA.gameObject.pos, Vector.mult(rectA.size, 0.5));
        let minB = Vector.sub(rigiB.gameObject.pos, Vector.mult(rectB.size, 0.5));
        let maxB = Vector.add(rigiB.gameObject.pos, Vector.mult(rectB.size, 0.5));
        let maxCorner = Vector.sub(maxA, minB);
        let minCorner = Vector.sub(minA, maxB);
        let col = maxCorner.x > 0 && maxCorner.y > 0 && minCorner.x < 0 && minCorner.y < 0;
        if (!col)
            return;
        minCorner.x = Math.abs(minCorner.x);
        minCorner.y = Math.abs(minCorner.y);
        maxCorner.x = Math.abs(maxCorner.x);
        maxCorner.y = Math.abs(maxCorner.y);
        let depth = Math.min(minCorner.x, minCorner.y, maxCorner.x, maxCorner.y);
        let normal = new Vector();
        let point = new Vector();
        switch (depth) {
            case minCorner.x:
                normal.x = -1;
                point = minB.y > minA.y ? new Vector(minB.x + rectB.size.x, minB.y) : new Vector(maxB);
                break;
            case minCorner.y:
                normal.y = -1;
                point = minB.x > minA.x ? new Vector(minB.x, minB.y + rectB.size.y) : new Vector(maxB);
                break;
            case maxCorner.x:
                normal.x = 1;
                point = maxB.y < maxA.y ? new Vector(minB.x - rectB.size.x, minB.y) : new Vector(minB);
                break;
            case maxCorner.y:
                normal.y = 1;
                point = maxB.x < maxA.x ? new Vector(minB.x, minB.y - rectB.size.y) : new Vector(minB);
                break;
        }
        return new CollisionManifold(rigiA, rigiB, normal, point, depth);
    }
    static circleVsRect(rigiA, rigiB) {
        let circleA = rigiA.shape;
        let rectB = rigiB.shape;
        let circlePos = rigiA.gameObject.pos;
        let rectMin = Vector.sub(rigiB.gameObject.pos, Vector.mult(rectB.size, 0.5));
        let rectMax = Vector.add(rigiB.gameObject.pos, Vector.mult(rectB.size, 0.5));
        let closest = new Vector(clamp(circlePos.x, rectMin.x, rectMax.x), clamp(circlePos.y, rectMin.y, rectMax.y));
        let dir = Vector.sub(closest, circlePos);
        let dist = dir.mag();
        let depth = dist - circleA.radius;
        if (depth >= 0)
            return;
        let normal = dir.mult(1 / dist);
        let point = Vector.add(circlePos, Vector.mult(normal, circleA.radius));
        return new CollisionManifold(rigiA, rigiB, normal, point, -depth);
    }
    static circleVsCircle(rigiA, rigiB) {
        let circleA = rigiA.shape;
        let circleB = rigiB.shape;
        let dir = Vector.sub(rigiB.gameObject.pos, rigiA.gameObject.pos);
        let dist = dir.mag();
        let depth = dist - (circleA.radius + circleB.radius);
        if (depth >= 0)
            return;
        let normal = Vector.mult(dir, 1 / dist);
        let point = Vector.add(rigiA.gameObject.pos, Vector.mult(normal, circleA.radius));
        return new CollisionManifold(rigiA, rigiB, normal, point, -depth);
    }
    // pos: center of the rect
    static rayVsRect(origin, direction, pos, size) {
        let normal = createVector();
        let point = createVector();
        let invDir = createVector(1. / direction.x, 1. / direction.y);
        let corner = Vector.sub(pos, Vector.mult(size, 0.5));
        let tNear = Vector.sub(corner, origin);
        tNear.x *= invDir.x;
        tNear.y *= invDir.y;
        let tFar = Vector.sub(Vector.add(corner, size), origin);
        tFar.x *= invDir.x;
        tFar.y *= invDir.y;
        if (isNaN(tFar.y) || isNaN(tFar.x))
            return null;
        if (isNaN(tNear.y) || isNaN(tNear.x))
            return null;
        if (tNear.x > tFar.x) {
            let tmp = tNear.x;
            tNear.x = tFar.x;
            tFar.x = tmp;
        }
        if (tNear.y > tFar.y) {
            let tmp = tNear.y;
            tNear.y = tFar.y;
            tFar.y = tmp;
        }
        if (tNear.x > tFar.y || tNear.y > tFar.y)
            return null;
        let tHitNear = Math.max(tNear.x, tNear.y);
        let tHitFar = Math.min(tFar.x, tFar.y);
        if (tHitFar < 0)
            return null;
        point.add(origin);
        point.add(Vector.mult(direction, tHitNear));
        if (tNear.x > tNear.y) {
            if (invDir.x < 0)
                normal.set(1, 0);
            else
                normal.set(-1, 0);
        }
        else if (tNear.x < tNear.y) {
            if (invDir.y < 0)
                normal.set(0, 1);
            else
                normal.set(0, -1);
        }
        return {
            point: point,
            normal: normal,
            t: tHitNear,
            distance: tHitNear * direction.mag()
        };
    }
    static raycast(origin, direction, range = Infinity) {
        let out = null;
        let minDist = Infinity;
        for (const collider of Physics.physicsComponents) {
            let hit = collider.intersectRay(origin, direction);
            if (hit && hit.distance < minDist)
                out = hit;
        }
        if (out && out.distance >= 0 && out.distance <= range)
            return out;
        return null;
    }
}

interface RayHit {
    point: Vector;
    t: number;
    normal: Vector;
    distance: number;
}
interface BoxCollision {
    collider: BoxCollider;
    hit: RayHit;
}
declare abstract class PhysicsComponent extends Component {
    constructor();
    abstract intersectRay(origin: Vector, direction: Vector): RayHit | null;
    destroy(): void;
}
declare class BoxCollider extends PhysicsComponent {
    size: Vector;
    constructor(w: number, h: number);
    intersectRay(origin: Vector, direction: Vector): RayHit | null;
}
declare class DynamicBoxCollider extends BoxCollider {
    vel: Vector;
    applyGravity: boolean;
    private collisions;
    onCollisionEnter: (collision: BoxCollision) => void;
    onCollisionExit: (collision: BoxCollision) => void;
    constructor(w: number, h: number);
    update(): void;
    applyForce(...args: [x: number, y: number] | [v: Vector]): void;
    resolveCollisions(): void;
    detectCollision(other: BoxCollider): BoxCollision | null;
}
declare class Physics {
    static gravity: Vector;
    static physicsComponents: Array<PhysicsComponent>;
    static rayVsRect(origin: Vector, direction: Vector, pos: Vector, size: Vector): RayHit | null;
    static raycast(origin: Vector, direction: Vector, range?: number): RayHit | null;
}

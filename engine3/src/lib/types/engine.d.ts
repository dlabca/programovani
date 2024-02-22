declare let deltaTime: number;
declare let width: number, height: number;
declare function start(): void;
declare class Engine {
    static lastUpdate: number;
    static interval: number;
    static inverseFramerate: number;
    static objects: Array<GameObject>;
    static running: boolean;
    static start(): void;
    static update(): void;
}
declare class GameObject {
    pos: Vector;
    components: Array<Component>;
    destroyed: boolean;
    constructor(x: number, y: number);
    start(): void;
    update(): void;
    addComponent(component: Component): this;
    getComponent<T extends Component>(type: {
        new (): T;
    }): T | undefined;
    destroy(): void;
}
declare abstract class Component {
    gameObject: GameObject;
    destroyed: boolean;
    constructor();
    getComponent<T extends Component>(type: {
        new (): T;
    }): T | undefined;
    start(): void;
    update(): void;
    destroy(): void;
}

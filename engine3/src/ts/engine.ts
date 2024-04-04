
let deltaTime = 0;
const fixedDeltaTime = 0.01;
let extraFixedTime = 0;
let width: number = 400, height: number = 400;

function start() {
    let canvas = document.createElement("canvas");
    document.body.insertBefore(canvas, null);

    canvas.width = width;
    canvas.height = height;
    canvas.style.background = "#333333";

    let ctx = canvas.getContext('2d');
    if (ctx == null) throw "Canvas rendering context not found!"
    Draw.ctx = ctx;

    Engine.start();

    document.body.addEventListener("keydown", e => {
        Input.keys[e.key] = true;
    });

    document.body.addEventListener("keyup", e => {
        Input.keys[e.key] = false;
    });

    Engine.inverseFramerate = 1000/60.0;
    Engine.interval = setInterval(Engine.update, Engine.inverseFramerate);
}

class Engine {

    static lastUpdate: number = Date.now();
    static interval: number;
    static inverseFramerate: number;

    static objects: Array<GameObject> = [];

    static running: boolean = false;

    static start() {
        //@ts-ignore
        setup();

        for (const gameObject of Engine.objects) {
            gameObject.start();
        }

        Engine.running = true;
    }

    static update() {

        let now = Date.now();
        deltaTime = (now - Engine.lastUpdate) / 1000.0;
        Engine.lastUpdate = now;
        
        extraFixedTime += deltaTime;
        while (extraFixedTime >= fixedDeltaTime) {
            Physics.update();

            for (const object of Engine.objects) {
                object.fixedUpdate();
            }

            extraFixedTime -= fixedDeltaTime;
        }

        for (let i = 0; i < Engine.objects.length; i++) {
            const gameObject = Engine.objects[i];
            if (gameObject.destroyed)
                Engine.objects.splice(i--, 1);
            
            gameObject.update();
        }
        //@ts-ignore
        draw();
        Input.update();

        Renderer.render();
    }
}

class GameObject {

    pos: Vector;
    components: Array<Component>;
    destroyed = false;

    constructor(x: number, y: number) {
        Engine.objects.push(this);

        this.pos = createVector(x, y);
        this.components = [];
    }

    start() {
        for (const component of this.components) {
            component.start();
        }
    }

    update() {
        for (let i = 0; i < this.components.length; i++) {
            const component = this.components[i];
            if (component.destroyed) {
                this.components.splice(i--, 1);
                component.destroy();
            } else
                component.update();
        }
    }

    fixedUpdate() {
        for (const component of this.components) {
            component.fixedUpdate();
        }
    }

    onCollisionEnter(col: Collision) {
        for (const component of this.components) {
            component.onCollisionEnter(col);
        }
    }

    onCollisionExit(col: Collision) {
        for (const component of this.components) {
            component.onCollisionExit(col);
        }
    }

    addComponent(component: Component) {
        this.components.push(component);
        component.gameObject = this;

        if (Engine.running) component.start();

        return this;
    }

    getComponent<T extends Component>(type: { new(): T }) {
        for (const component of this.components) {
            if (component instanceof type) return component;
        }
    }

    destroy() {
        this.destroyed = true;
        for (const component of this.components)
            component.destroyed = true;
    }

    static findObjectsOfType<T extends Component>(type: { new(): T }): Array<T> {
        let out: Array<T> = [];
        for (const object of Engine.objects) {
            let component = object.getComponent(type);
            if (component) out.push(component);
        }
        return out;
    }
}

abstract class Component {

    gameObject!: GameObject;
    destroyed = false;

    constructor() {}

    getComponent<T extends Component>(type: { new(): T }) { return this.gameObject.getComponent(type); }

    start() {}
    update() {}
    fixedUpdate() {}
    destroy() {}
    onCollisionEnter(col: Collision) {}
    onCollisionExit(col: Collision) {}
}
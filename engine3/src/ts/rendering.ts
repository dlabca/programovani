class Renderer {
    static renderers: Array<RendererComponent> = [];

    static render() {
        for (const renderer of Renderer.renderers) {
            if (renderer.enabled)
                renderer.render();
        }
        Draw.settings.transform.clear();
    }
}

abstract class RendererComponent extends Component {

    constructor() {
        super();
        Renderer.renderers.push(this);
    }

    abstract settings(): void;
    abstract draw(): void;

    render() {
        push();
        this.settings();
        this.draw();
        pop();
    }

    destroy(): void {
        Renderer.renderers.splice(Renderer.renderers.indexOf(this), 1);
    }
}

class RectRenderer extends RendererComponent {

    size: Vector;
    drawSettings = () => {};

    constructor(w: number, h: number, drawSettings: () => void = () => {}) {
        super();
        this.size = createVector(w, h);
        this.drawSettings = drawSettings;
    }

    settings(): void {
        this.drawSettings();
    }

    draw() {
        rect(this.gameObject.pos.x - this.size.x / 2, this.gameObject.pos.y - this.size.y / 2, this.size.x, this.size.y);
    }
}

class ShapeRenderer extends RendererComponent {

    shape!: Shape;

    start(): void {
        let shape = <Shape>this.getComponent(<any>Shape);
        if (!shape) {
            console.warn("An object with a ShapeRenderer component must also have a shape component");
            return;
        }
        this.shape = shape;
    }

    settings(): void {
        this.shape.settings();
    }

    draw(): void {
        this.shape.draw();
    }
}
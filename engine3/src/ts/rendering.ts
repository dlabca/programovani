class Renderer {
    static renderers: Array<RendererComponent> = [];

    static render() {
        for (const renderer of Renderer.renderers) {
            renderer.render();
        }
        Draw.settings.transform.clear();
    }
}

abstract class RendererComponent extends Component {

    settings: () => void;

    constructor(drawSettings = () => fill(255)) {
        super();
        Renderer.renderers.push(this);
        this.settings = drawSettings;
    }

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

    constructor(w: number, h: number, drawSettings = () => fill(255)) {
        super(drawSettings);
        this.size = createVector(w, h);
    }

    draw() {
        rect(this.gameObject.pos.x - this.size.x / 2, this.gameObject.pos.y - this.size.y / 2, this.size.x, this.size.y);
    }
}
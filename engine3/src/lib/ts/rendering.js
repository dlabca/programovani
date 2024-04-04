"use strict";
class Renderer {
    static renderers = [];
    static render() {
        for (const renderer of Renderer.renderers) {
            renderer.render();
        }
        Draw.settings.transform.clear();
    }
}
class RendererComponent extends Component {
    constructor() {
        super();
        Renderer.renderers.push(this);
    }
    render() {
        push();
        this.settings();
        this.draw();
        pop();
    }
    destroy() {
        Renderer.renderers.splice(Renderer.renderers.indexOf(this), 1);
    }
}
class RectRenderer extends RendererComponent {
    size;
    drawSettings = () => { };
    constructor(w, h, drawSettings = () => { }) {
        super();
        this.size = createVector(w, h);
        this.drawSettings = drawSettings;
    }
    settings() {
        this.drawSettings();
    }
    draw() {
        rect(this.gameObject.pos.x - this.size.x / 2, this.gameObject.pos.y - this.size.y / 2, this.size.x, this.size.y);
    }
}
class ShapeRenderer extends RendererComponent {
    shape;
    start() {
        let shape = this.getComponent(Shape);
        if (!shape) {
            console.warn("An object with a ShapeRenderer component must also have a shape component");
            return;
        }
        this.shape = shape;
    }
    settings() {
        this.shape.settings();
    }
    draw() {
        this.shape.draw();
    }
}

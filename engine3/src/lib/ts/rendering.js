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
    settings;
    constructor(drawSettings = () => fill(255)) {
        super();
        Renderer.renderers.push(this);
        this.settings = drawSettings;
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
    constructor(w, h, drawSettings = () => fill(255)) {
        super(drawSettings);
        this.size = createVector(w, h);
    }
    draw() {
        rect(this.gameObject.pos.x - this.size.x / 2, this.gameObject.pos.y - this.size.y / 2, this.size.x, this.size.y);
    }
}

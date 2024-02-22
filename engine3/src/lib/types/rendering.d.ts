declare class Renderer {
    static renderers: Array<RendererComponent>;
    static render(): void;
}
declare abstract class RendererComponent extends Component {
    settings: () => void;
    constructor(drawSettings?: () => void);
    abstract draw(): void;
    render(): void;
    destroy(): void;
}
declare class RectRenderer extends RendererComponent {
    size: Vector;
    constructor(w: number, h: number, drawSettings?: () => void);
    draw(): void;
}

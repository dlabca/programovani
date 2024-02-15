let seznamkruhu = [];

class Circle {
    constructor(x,y,size){
        this.x = x
        this.y = y
        this.size = size
    }

    draw() {
        circle(this.x,this.y,this.size)
    }
}

function setup() {
    createCanvas(1500, 700);
    background(51);

    for (let i = 0; i < 100; i++) {
        let x = random(width);
        let y = random(height);
        seznamkruhu.push(new Circle(x,y, 30))

    }
}

function draw() {
    for (const circle of seznamkruhu) {
        circle.draw()
    }
}
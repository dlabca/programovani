let grid = [];
const cellSize = 20;
let gridW, gridH;

Cell.prototype.init = function() {
    this.walls = [true, true, true, true];
    this.visited = false;
}

Cell.prototype.draw = function() {
    let pos = createVector(this.x * cellSize, this.y * cellSize);
    let dir = createVector(cellSize, 0);
    for (let i = 0; i < this.walls.length; i++) {        
        if (this.walls[i])
            line(pos.x, pos.y, pos.x + dir.x, pos.y + dir.y);
        pos.add(dir);
        dir.set(-dir.y, dir.x);
    }
}

Cell.prototype.breakWall = function(other) {
    let dist = abs(other.x - this.x) + abs(other.y - this.y);
    if (dist != 1) throw `Cell at (${this.x}, ${this.y}) and Cell at (${other.x}, ${other.y}) aren't neighbors`;

    if (other.x > this.x)
        this.walls[1] = other.walls[3] = false;
    else if (other.x < this.x)
        this.walls[3] = other.walls[1] = false;
    else if (other.y > this.y)
        this.walls[2] = other.walls[0] = false;
    else if (other.y < this.y)
        this.walls[0] = other.walls[2] = false;
    else
        throw `Bro I don't even know what's wrong here...`;
}

function setup() {
    createCanvas(400, 400);

    gridW = width / cellSize;
    gridH = height / cellSize;

    grid = new Array(gridW);
    for (let x = 0; x < gridW; x++) {
        grid[x] = new Array(gridH);
        for (let y = 0; y < gridH; y++) {
            grid[x][y] = new Cell(x, y);
            grid[x][y].init();
        }
    }

    generate();
}

function getCell(x, y) {
    if (x < 0 || y < 0 || x >= gridW || y >= gridH) return false;
    return grid[x][y];
}

function generate() {
    let current = grid[floor(random(gridW))][floor(random(gridH))];
    current.visited = true;
    let stack = [current];

    while(true) {
        let neighbor = current.getNeighbor();
        if (neighbor) {
            neighbor.visited = true;
            current.breakWall(neighbor);
            current = neighbor;
            stack.push(current);
        }
        else if (stack.length > 0) {
            current = stack.pop();
        }
        else break;
    }
}

function draw() {
    for (let x = 0; x < gridW; x++) {
        for (let y = 0; y < gridH; y++) {
            grid[x][y].draw();
        }
    }
    noLoop();
}
const playerSize = 30;
const playerSpeed = 5;

const enemySize = 30;

var player;
var enemySpeed = 3;
var enemyes = []

function setup() {
    createCanvas(1520, 750);
    player = createVector(width / 2, height / 2);
    for(var i = 0; i < 100; i++){
        var randompos = createVector(random(width), random(height));
        var enemy = new Enemy(randompos);
        enemyes.push (enemy);
    }
}

function draw() {
    background(220);

    var dir = createVector();
    if (Input.keyPressed('a')) dir.x -= 1;
    if (Input.keyPressed('d')) dir.x += 1;
    if (Input.keyPressed('w')) dir.y -= 1;
    if (Input.keyPressed('s')) dir.y += 1;
    dir.setMag(playerSpeed);
    player.add(dir);
    fill(255);
    circle(player.x, player.y, playerSize);
    for(var enemy of enemyes)
    enemy.update()
    for(var enemy of enemyes)
    enemy.draw()
   
}
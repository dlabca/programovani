class Enemy {
    constructor(pos){
        this.pos = pos;
    }
update(){
    var edir = p5.Vector.sub(player,this.pos);
    edir.setMag(3);
    enemy.add(edir);
}    
draw() {
    fill(255, 0, 0);
    circle(enemy.x, enemy.y, enemySize);
}
}

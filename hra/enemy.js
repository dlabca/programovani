class Enemy {
    constructor(pos){
        this.pos = pos.copy();
    }

    update(){
        var edir = p5.Vector.sub(player, this.pos);
        edir.setMag(3);
        this.pos.add(edir);
    }    

    draw() {
        fill(255, 0, 0);
        circle(this.pos.x, this.pos.y, enemySize);
    }
}

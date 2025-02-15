using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dino
{
    class Cactus
    {
        
        float x;
        float y;
        float width = 40;
        float height = 100;
        float speed = 10 * 60;
        public Cactus(float x){
            this.x = x;
            y = 300 - height + 51;
        }
        public void Update(float delta){
            x -= speed * delta;
           // x = Mouse.GetState().X;
           if (x < -width) x = 800;     
        }
        public void Draw(Game1 g){
            //g.DrawRect(x,y,width,height, Color.Green);
            Drawing.Draw(Sprite.Cact1,new Vector2(x,y),2);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Dino
{
    public enum Sprite
    {
        Dino,
        DinoRun1,
        DinoRun2,
        Ptero1,
        Ptero2,
        Cact1,
        Cact2,
        Cact3,
        Cact4,
        Cact5,
        Cact6,
        Ground
    }

    public static class Drawing
    {
        static SpriteBatch spriteBatch;
        static Texture2D sprite;

        public static Dictionary<Sprite, Rectangle> sprites = new()
        {
            { Sprite.Dino, new Rectangle(669, 0, 44, 48) },
            { Sprite.DinoRun1, new Rectangle(757, 0, 44, 48) },
            { Sprite.DinoRun2, new Rectangle(801, 0, 44, 48) },
            { Sprite.Ptero1, new Rectangle(130, 0, 46, 48) },
            { Sprite.Ptero2, new Rectangle(176, 0, 46, 48) },
            { Sprite.Cact1, new Rectangle(223, 0, 17, 51) },
            { Sprite.Cact2, new Rectangle(240, 0, 34, 51) },
            { Sprite.Cact3, new Rectangle(274, 0, 51, 51) },
            { Sprite.Cact4, new Rectangle(326, 0, 25, 51) },
            { Sprite.Cact5, new Rectangle(351, 0, 50, 51) },
            { Sprite.Cact6, new Rectangle(401, 0, 75, 51) },
            { Sprite.Ground, new Rectangle(1, 52, 1200, 13) }
        };

        public static void Initialize(SpriteBatch spriteBatch, Texture2D sprite)
        {
            Drawing.spriteBatch = spriteBatch;
            Drawing.sprite = sprite;
        }

        public static void Draw(Sprite sprite, Vector2 position, float scale = 1)
        {
            Rectangle rect = sprites[sprite];
            spriteBatch.Draw(Drawing.sprite, new Rectangle((int)position.X, (int)position.Y, (int)(rect.Width * scale), (int)(rect.Height * scale)), rect, Color.White);
        }
    }
}

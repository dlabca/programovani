using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dino
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D sprite;
        Texture2D tex;
        float posY = 0;
        float velY;
        float jumpForce = 1000;
        float gravity = 70;
        List<Cactus> cacti = new List<Cactus>();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            tex = new Texture2D(GraphicsDevice, 1, 1);
            tex.SetData(new Color[] { Color.White });
            cacti.Add(new Cactus(800));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Drawing.Initialize(_spriteBatch, Content.Load<Texture2D>("sprite"));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float delta = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && posY == 0)
            {
                velY = -jumpForce;
            }
            velY += gravity;
            posY += velY * delta;
            if (posY > 0)
            {
                posY = 0;
                velY = 0;
            }
            foreach (Cactus cactus in cacti)
            {
                cactus.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(200, 200, 200));


            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            //_spriteBatch.Draw(tex, new Rectangle(30, 300 + (int)posY, 40, 40), Color.Blue);
            Drawing.Draw(Sprite.Dino, new Vector2(30, 300 + (int)posY), 1);

            //Drawing.Draw(Sprite.Dino, Vector2.Zero, 3);
            foreach (Cactus cactus in cacti)
            {
                cactus.Draw(this);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        public void DrawRect(float x, float y, float width, float height, Color color)
        {
            _spriteBatch.Draw(tex, new Rectangle((int)x, (int)y, (int)width, (int)height), color);
        }
    }
}

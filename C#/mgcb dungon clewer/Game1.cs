using System;
using System.Security.Cryptography.X509Certificates;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mgcb_dungon_clewer
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D texture;
        Texture2D pixel;
        SpriteFont Font;
        int height = 720;
        int width = 1280;
        private Button myButton;
        bool mapa_zobrazena;
        public static int mapCellSize;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = height,
                PreferredBackBufferWidth = width
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });
            Window.AllowUserResizing = true;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("Fonts/Font");
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
            // V LoadContent přidej tento kód


            myButton = new Button(texture, Font, new Rectangle(50, 50, 100, 15), "Klikni na mě", Color.DarkBlue, Color.White);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (myButton.IsClicked())
            {
                // Akce po kliknutí
                mapa_zobrazena = true;
            }


            base.Update(gameTime);
        }
        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            // TODO: Add your drawing code here

            DrawLine(_spriteBatch, new Vector2(width / 3, 0), new Vector2(width / 3, height), Color.Black, 1f);
            DrawLine(_spriteBatch, new Vector2(2 * width / 3, 0), new Vector2(2 * width / 3, height), Color.Black, 1f);
            fill = Color.Blue;
            if (mapa_zobrazena == true)
            {
                for (int i = 0; i < width / 100; i++)
                {
                    for (int j = 0; j < height / 100; j++)
                    {
                        DrawRect(i * 100, j * 100, 100, 100);
                    }
                }
            }

            myButton.Draw(_spriteBatch);

            //_spriteBatch.DrawString(Font, "Hello, World!", new Vector2(100, 100), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public static Color fill = Color.Black;
        public static Color stroke = Color.Black;
        public void DrawRect(int x, int y, int width, int height)
        {
            _spriteBatch.Draw(texture, new Rectangle(x, y, width, height), stroke);
            _spriteBatch.Draw(texture, new Rectangle(x + 1, y + 1, width - 2, height - 2), fill);
        }
        public void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float thickness)
        {
            // Vypočítání délky a úhlu čáry
            var distance = Vector2.Distance(start, end);
            var angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);

            // Vykreslení čáry pomocí textury pixelu
            spriteBatch.Draw(pixel, start, null, color, angle, Vector2.Zero, new Vector2(distance, thickness), SpriteEffects.None, 0);
        }
    }
}
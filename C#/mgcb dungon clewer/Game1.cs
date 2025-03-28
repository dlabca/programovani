using System;


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
        private Button myButton;
        bool mapa_zobrazena;
        public static int mapCellSize;
        int prevousheight;
        int prevouswidth;
        int heigth = 720;
        int width = 1280;
        float pomer = 16f / 9f;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = heigth,
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

        KeyboardState oldState;
        protected override void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
            var mouse = Mouse.GetState();

            // Zavření hry
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Escape))
                Exit();

            // Přepínání fullscreen režimu pomocí klávesy F11
            if (keyboard.IsKeyDown(Keys.F11) && !oldState.IsKeyDown(Keys.F11) && !_graphics.IsFullScreen)
            {
                _graphics.PreferredBackBufferWidth = 1920;
                _graphics.PreferredBackBufferHeight = 1080;
                // _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                _graphics.IsFullScreen = true;
                _graphics.ApplyChanges();
            }
            else if (keyboard.IsKeyDown(Keys.F11) && !oldState.IsKeyDown(Keys.F11) && _graphics.IsFullScreen)
            {
                _graphics.PreferredBackBufferWidth = width;
                _graphics.PreferredBackBufferHeight = heigth;
                _graphics.IsFullScreen = false;

                _graphics.ApplyChanges();
            }

            // Pokud není fullscreen, přizpůsobuje velikost podle poměru stran
            if (!_graphics.IsFullScreen)
            {
                heigth = Window.ClientBounds.Height;
                width = Window.ClientBounds.Width;

                if (heigth != prevousheight)
                {
                    width = (int)(heigth * pomer);
                    _graphics.PreferredBackBufferHeight = heigth;
                    _graphics.PreferredBackBufferWidth = width;
                    prevousheight = heigth;
                }
                else if (width != prevouswidth)
                {
                    heigth = (int)(width / pomer);
                    _graphics.PreferredBackBufferWidth = width;
                    _graphics.PreferredBackBufferHeight = heigth;
                    prevouswidth = width;
                }

                // Pouze jednou použijeme ApplyChanges po úpravách
                if (heigth != prevousheight || width != prevouswidth)
                {
                    _graphics.ApplyChanges();
                }
            }

            // Kliknutí na tlačítko
            if (myButton.IsClicked())
            {
                mapa_zobrazena = !mapa_zobrazena;
            }

            base.Update(gameTime);

            oldState = keyboard;
        }




        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            // TODO: Add your drawing code here

            //  DrawLine(_spriteBatch, new Vector2(width / 3, 0), new Vector2(width / 3, heigth), Color.Black, 1f);
            //DrawLine(_spriteBatch, new Vector2(2 * width / 3, 0), new Vector2(2 * width / 3, heigth), Color.Black, 1f);
            if (mapa_zobrazena == true)
            {
                fill = Color.Blue;

                for (int i = 0; i < width / 100; i++)
                {
                    for (int j = 0; j < heigth / 100; j++)
                    {
                        DrawRect(i * 100, j * 100, 100, 100, stroke, fill);
                    }
                }
            }

            myButton.Draw(_spriteBatch);

            _spriteBatch.DrawString(Font,_graphics.IsFullScreen? "full":"nefull" , new Vector2(100, 100), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public static Color fill = Color.Black;
        public static Color stroke = Color.Black;
        public void DrawRect(int x, int y, int width, int height, Color stroke, Color fill)
        {
            _spriteBatch.Draw(texture, new Rectangle(x, y, width, height), stroke); // Vnější rámeček
            _spriteBatch.Draw(texture, new Rectangle(x + 1, y + 1, width - 2, height - 2), fill); // Výplň
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
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WireWorld
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D texture;

        CellType[,] cells;
        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            // TODO: Add your initialization logic here
            cells = new CellType[40, 24];
            cells[2,3] = CellType.Head;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            /*fill = Color.Yellow;
            DrawRect(100, 100, 200, 50);*/
            fill = Color.Blue;
            for (int y = 0; y < 24; y++)
            {
                for (int x = 0; x < 40; x++)
                {
                    if(cells[x, y] == CellType.Empty){
                        fill = Color.Blue;
                    }
                    else if (cells[x, y] == CellType.Head)
                    {
                        fill = Color.Yellow;
                    }
                    else if (cells[x, y] == CellType.Wire)
                    {
                        fill = Color.White;
                    }
                    else if (cells[x, y] == CellType.Tail)
                    {
                        fill = Color.Purple;
                    }
                    DrawRect(x * 20, y * 20, 20, 20);
                }

            }

            // TODO: Add your drawing code here

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
    }
}

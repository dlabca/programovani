using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace test_mgcb_1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.AllowUserResizing = true;

            // Přidání události na změnu velikosti okna
            Window.ClientSizeChanged += OnClientSizeChanged;
        }

        private void OnClientSizeChanged(object sender, System.EventArgs e)
        {
            // Získání aktuální velikosti okna
            int width = Window.ClientBounds.Width;
            int height = Window.ClientBounds.Height;

            // Porovnání velikosti okna s rozlišením obrazovky
            if (width == GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width &&
                height == GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
            {
                // Okno je ve fullscreen režimu
                System.Console.WriteLine("Okno je nyní v systémovém fullscreen režimu.");
            }
            else
            {
                // Zobrazení aktuální velikosti okna
                System.Console.WriteLine($"Velikost okna: {width}x{height}");
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TextInputExample
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont _font;
        private string _inputText = "";
        private string _outputText = "";
        //private Texture2D _exampleImage;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Načtení fontu (nezapomeňte přidat font do Content projektu)
            _font = Content.Load<SpriteFont>("Fonts/Arial");

            // Načtení obrázku
           // _exampleImage = Content.Load<Texture2D>("exampleImage");
        }

        protected override void Update(GameTime gameTime)
        {
            // Čtení klávesnice
            var keyboardState = Keyboard.GetState();

            foreach (var key in keyboardState.GetPressedKeys())
            {
                if (key == Keys.Back && _inputText.Length > 0)
                {
                    _inputText = _inputText[..^1]; // Mazání posledního znaku
                }
                else if (key == Keys.Enter)
                {
                    _outputText = $"Zadal jste: {_inputText}";
                    if (_inputText.ToLower() == "obrazek")
                    {
                        _outputText = "Vykresluji obrázek!";
                    }
                    _inputText = ""; // Vyčistění po stisknutí Enter
                }
                else if (key != Keys.Back && key != Keys.Enter)
                {
                    _inputText += key.ToString();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Vykreslení textu a vstupu
            _spriteBatch.DrawString(_font, "Zadejte text: " + _inputText, new Vector2(50, 50), Color.White);
            _spriteBatch.DrawString(_font, _outputText, new Vector2(50, 100), Color.Yellow);

            // Podle vstupu vykreslení obrázku
            if (_outputText.Contains("obrázek", System.StringComparison.OrdinalIgnoreCase))
            {
                //_spriteBatch.Draw(_exampleImage, new Vector2(50, 150), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

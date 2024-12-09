using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace kalkulačka;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _buttonTexture;
    private SpriteFont _font;
    private Button _button;
    int height,width;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Window.AllowUserResizing = true;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Načtení textury a písma
        _buttonTexture = new Texture2D(GraphicsDevice, 1, 1);
        _buttonTexture.SetData(new[] { Color.White }); // Jednoduchá bílá textura pro tlačítko

        _font = Content.Load<SpriteFont>("Fonts/File"); // Nahraď za název svého fontu

        // Vytvoření tlačítka
        _button = new Button(_buttonTexture, _font, new Rectangle(200, 150, 200, 50), "Klikni mě", Color.Blue, Color.White);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        

        // TODO: Add your update logic here
        if (_button.IsClicked())
        {

        }
        if (height != GraphicsDevice.Viewport.Height || width != GraphicsDevice.Viewport.Width)
        {
            height = GraphicsDevice.Viewport.Height;
            width = GraphicsDevice.Viewport.Width;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _button.Draw(_spriteBatch);

        // TODO: Add your drawing code here
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}

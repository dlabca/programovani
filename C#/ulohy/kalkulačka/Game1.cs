using System.Security.Cryptography.X509Certificates;
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
    int height, width;
    Button[,] buttons = new Button[4, 4];
    string[,] buttonLabels = new string[,]
    {
        { "7", "8", "9", "/" },
        { "4", "5", "6", "*" },
        { "1", "2", "3", "-" },
        { "0", ",", "=", "+" }
    };
    int buttonWidth;
    int buttonHeight;


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

        _buttonTexture = new Texture2D(GraphicsDevice, 1, 1);
        _buttonTexture.SetData(new[] { Color.White }); // Jednoduchá bílá textura pro tlačítka
        height = GraphicsDevice.Viewport.Height;
        width = GraphicsDevice.Viewport.Width;

        _font = Content.Load<SpriteFont>("Fonts/File");
        buttonWidth = width / 4;
        buttonHeight = height / 3 * 2 / 4;




        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                int x = col * buttonWidth; // Vzdálenost mezi tlačítky
                int y = row * buttonHeight + height / 3;
                buttons[row, col] = new Button(_buttonTexture, _font, new Rectangle(x, y, buttonWidth, buttonHeight), buttonLabels[row, col], Color.Blue, Color.White);
            }
        }
    }



    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();


        // TODO: Add your update logic here

        if (height != GraphicsDevice.Viewport.Height || width != GraphicsDevice.Viewport.Width)
        {
            height = GraphicsDevice.Viewport.Height;
            width = GraphicsDevice.Viewport.Width;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    int x = col * buttonWidth; // Vzdálenost mezi tlačítky
                    int y = row * buttonHeight + height / 3;
                    buttons[row, col] = new Button(_buttonTexture, _font, new Rectangle(x, y, buttonWidth, buttonHeight), buttonLabels[row, col], Color.Blue, Color.White);
                }
            }

        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                buttons[i, j].Draw(_spriteBatch);
            }
        }

        // TODO: Add your drawing code here
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D texture;
    int height = 720;
    int width = 1280;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferHeight = 720,
            PreferredBackBufferWidth = 1280
        };
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        texture = new Texture2D(GraphicsDevice, 1, 1);
        texture.SetData(new Color[] { Color.White });
        // TODO: Add your initialization logic here

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
        var mouse = Mouse.GetState();
        var keyboard = Keyboard.GetState();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        _spriteBatch.Begin();

        // TODO: Add your drawing code here
        DrawRect(width / 3, 0, 1, height);
        DrawRect(width / 3 * 2, 0, 1, height);
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace GameEngine
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Random _random = new Random();

        private enum RoomType { Empty, Enemy, Treasure, Shop }
        private enum EnemyType { Goblin, Orc, Dragon }
        private enum ItemType { Sword, Shield, Potion }

        private Texture2D[] _roomTextures;
        private List<ItemType> _inventory = new List<ItemType>();

        private Button _button1;
        private Button _button2;

        private Rectangle[,] _gridCells;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _roomTextures = new Texture2D[Enum.GetValues(typeof(RoomType)).Length];
            for (int i = 0; i < _roomTextures.Length; i++)
            {
                _roomTextures[i] = Content.Load<Texture2D>("room_" + i);
            }

            _gridCells = new Rectangle[4, 6];
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    _gridCells[x, y] = new Rectangle(GraphicsDevice.Viewport.Width / 3 * x, GraphicsDevice.Viewport.Height / 6 * y, GraphicsDevice.Viewport.Width / 3 / 4, GraphicsDevice.Viewport.Height / 6);
                }
            }

            _button1 = new Button(new Rectangle(GraphicsDevice.Viewport.Width * 2 / 3, 0, GraphicsDevice.Viewport.Width / 3, 50), "Button 1", () => { /* button 1 click action */ });
            _button2 = new Button(new Rectangle(GraphicsDevice.Viewport.Width * 2 / 3, 50, GraphicsDevice.Viewport.Width / 3, 50), "Button 2", () => { /* button 2 click action */ });
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            // Update game logic here
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw grid
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    DrawingHelper.DrawRect(_spriteBatch, _gridCells[x, y], Color.White);
                }
            }

            // Draw rooms
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    RoomType roomType = GetRandomRoomType();
                    _spriteBatch.Draw(_roomTextures[(int)roomType], _gridCells[x, y], Color.White);
                }
            }

            // Draw inventory
            int inventoryX = GraphicsDevice.Viewport.Width / 3;
            int inventoryY = 0;
            int inventoryWidth = GraphicsDevice.Viewport.Width / 3;
            int inventoryHeight = GraphicsDevice.Viewport.Height;

            DrawingHelper.DrawRect(_spriteBatch, new Rectangle(inventoryX, inventoryY, inventoryWidth, inventoryHeight), Color.White);

            int itemIndex = 0;
            foreach (ItemType item in _inventory)
            {
                Texture2D itemTexture = Content.Load<Texture2D>("item_" + item);
                _spriteBatch.Draw(itemTexture, new Rectangle(inventoryX + 10, inventoryY + 10 + itemIndex * 20, itemTexture.Width, itemTexture.Height), Color.White);
                itemIndex++;
            }

            // Draw control panel
            _button1.Draw(_spriteBatch, Color.White);
            _button2.Draw(_spriteBatch, Color.White);

            base.Draw(gameTime);
        }

        private RoomType GetRandomRoomType()
        {
            return (RoomType)_random.Next(Enum.GetValues(typeof(RoomType)).Length);
        }

        private EnemyType GetRandomEnemyType()
        {
            return (EnemyType)_random.Next(Enum.GetValues(typeof(EnemyType)).Length);
        }

        private ItemType GetRandomItemType()
        {
            return (ItemType)_random.Next(Enum.GetValues(typeof(ItemType)).Length);
        }
    }

    public static class DrawingHelper
    {
        public static void DrawRect(SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
texture.SetData(new[] { color });
            spriteBatch.Draw(texture, rect, color);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
        {
            Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            texture.SetData(new[] { color });
            spriteBatch.Draw(texture, new Rectangle((int)start.X, (int)start.Y, (int)(end.X - start.X), 1), color);
        }

        public static void DrawCircle(SpriteBatch spriteBatch, Vector2 center, float radius, Color color)
        {
            Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            texture.SetData(new[] { color });
            spriteBatch.Draw(texture, new Rectangle((int)center.X - (int)radius, (int)center.Y - (int)radius, (int)radius * 2, (int)radius * 2), color);
        }
    }

    public class Button
    {
        public Rectangle Bounds { get; set; }
        public string Text { get; set; }
        public Action ClickAction { get; set; }

        public Button(Rectangle bounds, string text, Action clickAction)
        {
            Bounds = bounds;
            Text = text;
            ClickAction = clickAction;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            DrawingHelper.DrawRect(spriteBatch, Bounds, color);
            spriteBatch.DrawString(Content.Load<SpriteFont>("font"), Text, new Vector2(Bounds.X + 10, Bounds.Y + 10), color);
        }

        public bool IsClicked(MouseState mouseState)
        {
            return mouseState.X >= Bounds.X && mouseState.X <= Bounds.X + Bounds.Width && mouseState.Y >= Bounds.Y && mouseState.Y <= Bounds.Y + Bounds.Height;
        }
    }
}
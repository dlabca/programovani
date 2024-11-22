using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game
{
    enum CellType
    {
        Empty,
        Wall,
    }

    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D texture;
        int height = 720;
        int width = 1280;
        int cellSize = 20;
        int playerSpeed = 2;
        int gridw, gridh;
        CellType[,] cells;
        Vector2 player;
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
        struct RectHit
        {
            public Vector2 normal;
            public float depth;
        }
        RectHit? overLapRect(float x, float y, float w, float px, float py)
        {
            RectHit hit;
            float dx = MathF.Abs(x - px);
            float dy = MathF.Abs(y - py);
            float dist = MathF.Max(dx, dy);
            hit.depth = w / 2f - dist;
            if (dist == dx) hit.normal = new Vector2(1, 0);
            else hit.normal = new Vector2(0, 1);

            hit.normal.X *= MathF.Sign(x - px);
            hit.normal.Y *= MathF.Sign(y - py);

            if (dist < w / 2)
            {
                return hit;
            }
            else
            {
                return null;
            }

        }
        protected override void Initialize()
        {
            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            // TODO: Add your initialization logic here
            gridw = width / cellSize;
            gridh = height / cellSize;
            player.X = width / 2;
            player.Y = height / 2;
            cells = new CellType[gridw, gridh];

            for (int Xn = 0; Xn < gridw; Xn++)
            {
                for (int Yn = 0; Yn < gridh; Yn++)
                {
                    var rnd = Random.Shared.NextDouble();
                    if (rnd < 0.5)
                    {
                        cells[Xn, Yn] = CellType.Empty;
                    }
                    else
                    {
                        cells[Xn, Yn] = CellType.Wall;
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                Rules();
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        List<(int, int)> neitghborDirs = new List<(int, int)>
                {
                    (0,1),
                    (0,-1),
                    (1,0),
                    (-1,0),
                    (1,1),
                    (-1,-1),
                    (1,-1),
                    (-1,1)
                };
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var keyboard = Keyboard.GetState();
            Vector2 move = new Vector2();
            if (keyboard.IsKeyDown(Keys.Up))
            {
                move.Y -= playerSpeed;
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                move.Y += playerSpeed;
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                move.X -= playerSpeed;
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                move.X += playerSpeed;
            }
            int tileX = (int)player.X / cellSize;
            int tileY = (int)player.Y / cellSize;
            foreach ((int dx, int dy) in neitghborDirs)
            {
                int i = tileX + dx;
                int j = tileY + dy;
                if (cells[i, j] == CellType.Empty) continue;
                float rectX = i * cellSize + cellSize / 2f;
                float rectY = j * cellSize + cellSize / 2f;
                float w = cellSize * 2;

                Vector2 nextPos = player + move;

                var collision = overLapRect(rectX, rectY, w, nextPos.X, nextPos.Y);

                if (collision is RectHit hit)
                {
                    move -= hit.normal * hit.depth;
                }
            }

            player += move;

            base.Update(gameTime);
        }
        void Rules()
        {
            CellType[,] nextCells = new CellType[gridw, gridh];
            for (int Xn = 0; Xn < gridw; Xn++)
            {
                for (int Yn = 0; Yn < gridh; Yn++)
                {
                    int neighbors = 0;
                    for (int X = -1; X <= 1; X++)
                    {
                        for (int Y = -1; Y <= 1; Y++)
                        {
                            int nx = Xn + X;
                            int ny = Yn + Y;
                            bool outOfBounds = nx < 0 || nx >= gridw || ny < 0 || ny >= gridh;
                            if (!outOfBounds && cells[nx, ny] == CellType.Empty && (Xn != 0 || Yn != 0))
                            {
                                neighbors++;
                            }
                        }
                    }

                    if (neighbors >= 5)
                    {
                        nextCells[Xn, Yn] = CellType.Empty;
                    }
                    else
                    {
                        nextCells[Xn, Yn] = CellType.Wall;
                    }
                }
            }
            cells = nextCells;
        }


        public static Color fill = Color.Black;
        public static Color stroke = Color.Black;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            for (int X = 0; X < gridw; X++)
            {
                for (int Y = 0; Y < gridh; Y++)
                {
                    if (cells[X, Y] == CellType.Empty)
                    {
                        fill = Color.White;
                    }
                    else
                    {
                        fill = Color.SaddleBrown;
                    }
                    DrawRect(X * cellSize, Y * cellSize, cellSize, cellSize);
                }
            }
            fill = Color.Red;
            DrawRect(player.X - cellSize / 2, player.Y - cellSize / 2, cellSize, cellSize);


            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public void DrawRect(float x, float y, int width, int height)
        {
            //_spriteBatch.Draw(texture, new Rectangle((int)x, (int)y, width, height), stroke);
            _spriteBatch.Draw(texture, new Rectangle((int)x, (int)y, width, height), fill);
        }
    }
}
using System;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WireWorld
{

    public enum CellType
    {
        Empty,
        Wire,
        Head,
        Tail,
    }

    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D texture;
        int height = 720;
        int width = 1280;
        int cellSize = 20;
        int gridw, gridh;
        int frame = 0;

        CellType[,] cells;
        public Game()
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
            gridw = width / cellSize;
            gridh = height / cellSize;
            cells = new CellType[gridw, gridh];

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

            (int x, int y) = mouse.Position;

            int mousecellposx = x / cellSize;
            int mousecellposy = y / cellSize;
            if (keyboard.IsKeyDown(Keys.S)) //SAFE
            {
                Console.WriteLine("Napište jméno soboru do kterého chcete uložit wireworld");
                string nameS = Console.ReadLine();
                using (StreamWriter sw = new StreamWriter(nameS, false))
                {
                    sw.WriteLine(gridw);
                    sw.WriteLine(gridh);
                    for (int swy = 0; swy < gridh; swy++)
                    {
                        for (int swx = 0; swx < gridw; swx++)
                        {
                            char chr = (char)((int)cells[swx, swy] + '0');
                            sw.Write(chr);
                        }
                    }
                }
            }
            if (keyboard.IsKeyDown(Keys.L)) //LOAD
            {
                Console.WriteLine("Zadejte jméno souboru ze kterého chcete nahrát wireworld");
                string nameL = Console.ReadLine();
                using (StreamReader sr = new StreamReader(nameL)){
                    gridw = int.Parse(sr.ReadLine());
                    gridh = int.Parse(sr.ReadLine());

                    cells = new CellType[gridw, gridh];

                    for (int swy = 0; swy < gridh; swy++)
                    {
                        for (int swx = 0; swx < gridw; swx++){
                            cells[swx, swy] = (CellType)sr.Read();
                        }
                    }
                }
            }
            if (keyboard.IsKeyDown(Keys.R))//RESET
            {
                cells = new CellType[gridw, gridh];
            }
            if (x < width && y < height && x > 0 && y > 0)
            {
                if (mouse.LeftButton == ButtonState.Pressed && keyboard.IsKeyUp(Keys.LeftControl))
                {
                    cells[mousecellposx, mousecellposy] = CellType.Wire;
                }
                if (mouse.RightButton == ButtonState.Pressed)
                {
                    cells[mousecellposx, mousecellposy] = CellType.Head;
                }
                if (mouse.LeftButton == ButtonState.Pressed && keyboard.IsKeyDown(Keys.LeftControl))
                {
                    cells[mousecellposx, mousecellposy] = CellType.Empty;
                }

            }
            frame++;
            if (frame >= 60)
            {
                frame = 0;
                Rules();
            }
            base.Update(gameTime);
        }

        void Rules()
        {
            CellType[,] nextCells = new CellType[gridw, gridh];
            for (int x = 0; x < gridw; x++)
            {
                for (int y = 0; y < gridh; y++)
                {
                    CellType prevCell = cells[x, y];
                    CellType nextCell = CellType.Empty;

                    if (prevCell == CellType.Head)
                        nextCell = CellType.Tail;
                    if (prevCell == CellType.Tail)
                        nextCell = CellType.Wire;
                    if (prevCell == CellType.Wire)
                    {
                        nextCell = CellType.Wire;
                        int neghbors = 0;
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                int nx = x + i;
                                nx = (nx + gridw) % gridw;
                                int ny = y + j;
                                ny = (ny + gridh) % gridh;
                                if (cells[nx, ny] == CellType.Head)
                                {
                                    neghbors++;
                                }
                            } 
                        }
                        if (neghbors == 1 || neghbors == 2)
                        {
                            nextCell = CellType.Head;
                        }
                    }

                    nextCells[x, y] = nextCell;
                }
            }

            cells = nextCells;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            /*fill = Color.Yellow;
            DrawRect(100, 100, 200, 50);*/
            fill = Color.Blue;
            for (int y = 0; y < gridh; y++)
            {
                for (int x = 0; x < gridw; x++)
                {
                    if (cells[x, y] == CellType.Empty)
                    {
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
                    DrawRect(x * cellSize, y * cellSize, cellSize, cellSize);
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

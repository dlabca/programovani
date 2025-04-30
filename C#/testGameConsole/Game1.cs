using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testGameConsole
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameConsole _console;
        private SpriteFont _font;
        public Color backroundColor = Color.CornflowerBlue;

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
            _font = Content.Load<SpriteFont>("Fonts/Arial"); // Ujisti se, že máš správnou fontu
            
            // Vytvoření konzole
            _console = new GameConsole(new Rectangle(20, 20, 760, 400), _font, GraphicsDevice);
            
            // Přiřazení metody pro zpracování příkazů
            _console.OnCommandEntered += ProcessCommand;
            
            // Úvodní zprávy
            _console.AddMessage("Vítejte v herní konzoli!");
            _console.AddMessage("Napište 'help' pro zobrazení dostupných příkazů.");
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            _console.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backroundColor);

            _spriteBatch.Begin();
            _console.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        // Metoda pro zpracování příkazů z konzole
        private void ProcessCommand(string command)
        {
            string cmd = command.ToLower().Trim();
            string[] args = cmd.Split(' ');

            switch (args[0])
            {
                case "help":
                    ShowHelp();
                    break;
                
                case "clear":
                case "cls":
                    ClearConsole();
                    break;
                
                case "exit":
                    _console.AddMessage("Pro ukončení stiskněte klávesu ESC");
                    break;
                
                case "color":
                    ChangeBackgroundColor(args);
                    break;
                
                case "echo":
                    if (args.Length > 1)
                    {
                        _console.AddMessage(string.Join(" ", args, 1, args.Length - 1));
                    }
                    break;
                
                case "version":
                    _console.AddMessage("Herní konzole v1.0");
                    break;
                
                default:
                    _console.AddMessage($"Neznámý příkaz: {cmd}");
                    _console.AddMessage("Napište 'help' pro seznam dostupných příkazů.");
                    break;
            }
        }

        private void ShowHelp()
        {
            _console.AddMessage("Dostupné příkazy:");
            _console.AddMessage("  help - zobrazí tuto nápovědu");
            _console.AddMessage("  clear / cls - vyčistí konzoli");
            _console.AddMessage("  exit - zobrazí informaci o ukončení hry");
            _console.AddMessage("  color [blue/red/green/default] - změní barvu pozadí");
            _console.AddMessage("  echo [text] - zobrazí zadaný text");
            _console.AddMessage("  version - zobrazí verzi aplikace");
        }

        private void ClearConsole()
        {
            // Vytvoříme novou instanci konzole se zachováním stávajících parametrů
            Rectangle rect = new Rectangle(20, 20, 760, 400);
            _console = new GameConsole(rect, _font, GraphicsDevice);
            _console.OnCommandEntered += ProcessCommand;
            _console.AddMessage("Konzole vyčištěna.");
        }

        private void ChangeBackgroundColor(string[] args)
        {
            if (args.Length < 2)
            {
                _console.AddMessage("Použití: color [blue/red/green/default]");
                return;
            }

            switch (args[1].ToLower())
            {
                case "blue":
                    backroundColor = Color.CornflowerBlue;
                    break;
                case "red":
                    backroundColor = Color.DarkRed;
                    break;
                case "green":
                    backroundColor = Color.ForestGreen;
                    break;
                case "default":
                case "reset":
                    backroundColor = Color.CornflowerBlue;
                    break;
                default:
                    _console.AddMessage("Neplatná barva. Použijte blue, red, green nebo default");
                    return;
            }

            GraphicsDevice.Clear(backroundColor);
            _console.AddMessage($"Barva pozadí změněna na {args[1]}");
        }
    }
} 
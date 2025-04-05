using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace testGameConsole
{
    public class GameConsole
    {
        private Rectangle _consoleRect;
        private SpriteFont _font;
        private List<string> _history;
        private List<string> _commandHistory;
        private int _commandHistoryIndex = -1;
        private int _scrollOffset = 0;
        private string _currentInput;
        private GraphicsDevice _graphicsDevice;
        private Texture2D _whiteTexture;
        private KeyboardState _previousKeyboardState;
        private float _keyRepeatTimer = 0f;
        private const float KEY_REPEAT_DELAY = 0.5f;
        private const float KEY_REPEAT_INTERVAL = 0.05f;
        private bool _isKeyRepeating = false;

        // Delegát pro zpracování příkazů
        public delegate void CommandHandler(string command);
        public event CommandHandler OnCommandEntered;

        public GameConsole(Rectangle consoleRect, SpriteFont font, GraphicsDevice graphicsDevice)
        {
            _consoleRect = consoleRect;
            _font = font;
            _graphicsDevice = graphicsDevice;
            _history = new List<string>();
            _commandHistory = new List<string>();
            _scrollOffset = 0;
            _currentInput = string.Empty;
            _previousKeyboardState = Keyboard.GetState();
            
            // Vytvoření bílé textury hned při inicializaci
            _whiteTexture = new Texture2D(_graphicsDevice, 1, 1);
            _whiteTexture.SetData(new Color[] { Color.White });
        }

        public void AddMessage(string message)
        {
            _history.Add(message);
            if (_history.Count > 100) // Zvýšený limit historie
            {
                _history.RemoveAt(0);
            }
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Zpracování speciálních kláves
            ProcessSpecialKeys(keyboardState, deltaTime);

            // Zpracování alfanumerických kláves pro vstup textu
            ProcessTextInput(keyboardState);

            _previousKeyboardState = keyboardState;
        }

        private void ProcessSpecialKeys(KeyboardState keyboardState, float deltaTime)
        {
            // Zpracování klávesy Enter pro potvrzení vstupu
            if (IsKeyPressed(keyboardState, Keys.Enter))
            {
                if (!string.IsNullOrWhiteSpace(_currentInput))
                {
                    string command = _currentInput.Trim();
                    AddMessage("> " + command);

                    // Přidání příkazu do historie
                    if (_commandHistory.Count == 0 || _commandHistory[_commandHistory.Count - 1] != command)
                    {
                        _commandHistory.Add(command);
                    }
                    _commandHistoryIndex = -1;

                    // Vyvolání události pro zpracování příkazu
                    OnCommandEntered?.Invoke(command);
                    
                    _currentInput = string.Empty;
                }
            }

            // Zpracování klávesy Backspace pro mazání znaků
            if (keyboardState.IsKeyDown(Keys.Back))
            {
                if (_previousKeyboardState.IsKeyUp(Keys.Back) || IsKeyRepeating(deltaTime))
                {
                    if (_currentInput.Length > 0)
                    {
                        _currentInput = _currentInput.Remove(_currentInput.Length - 1);
                    }
                }
            }

            // Šipka nahoru - předchozí příkaz z historie
            if (IsKeyPressed(keyboardState, Keys.Up) || 
                (keyboardState.IsKeyDown(Keys.Up) && IsKeyRepeating(deltaTime)))
            {
                if (_commandHistory.Count > 0)
                {
                    if (_commandHistoryIndex < _commandHistory.Count - 1)
                    {
                        _commandHistoryIndex++;
                    }
                    _currentInput = _commandHistory[_commandHistory.Count - 1 - _commandHistoryIndex];
                }
            }

            // Šipka dolů - následující příkaz z historie
            if (IsKeyPressed(keyboardState, Keys.Down) || 
                (keyboardState.IsKeyDown(Keys.Down) && IsKeyRepeating(deltaTime)))
            {
                if (_commandHistoryIndex > 0)
                {
                    _commandHistoryIndex--;
                    _currentInput = _commandHistory[_commandHistory.Count - 1 - _commandHistoryIndex];
                }
                else if (_commandHistoryIndex == 0)
                {
                    _commandHistoryIndex = -1;
                    _currentInput = string.Empty;
                }
            }

            // Posouvání historie zpráv
            if (IsKeyPressed(keyboardState, Keys.PageUp) || 
                (keyboardState.IsKeyDown(Keys.PageUp) && IsKeyRepeating(deltaTime)))
            {
                ScrollUp();
            }
            
            if (IsKeyPressed(keyboardState, Keys.PageDown) || 
                (keyboardState.IsKeyDown(Keys.PageDown) && IsKeyRepeating(deltaTime)))
            {
                ScrollDown();
            }
        }

        private bool IsKeyPressed(KeyboardState currentState, Keys key)
        {
            return currentState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
        }

        private bool IsKeyRepeating(float deltaTime)
        {
            if (_isKeyRepeating)
            {
                _keyRepeatTimer -= deltaTime;
                if (_keyRepeatTimer <= 0)
                {
                    _keyRepeatTimer = KEY_REPEAT_INTERVAL;
                    return true;
                }
            }
            else
            {
                _keyRepeatTimer -= deltaTime;
                if (_keyRepeatTimer <= 0)
                {
                    _isKeyRepeating = true;
                    _keyRepeatTimer = KEY_REPEAT_INTERVAL;
                    return true;
                }
            }
            return false;
        }

        private void ProcessTextInput(KeyboardState keyboardState)
        {
            foreach (Keys key in keyboardState.GetPressedKeys())
            {
                if (_previousKeyboardState.IsKeyUp(key))
                {
                    string character = KeyToChar(key, keyboardState);
                    if (!string.IsNullOrEmpty(character))
                    {
                        _currentInput += character;
                    }
                    
                    // Resetovat časovač opakování klávesy, pokud je stisknuto tlačítko
                    _keyRepeatTimer = KEY_REPEAT_DELAY;
                    _isKeyRepeating = false;
                }
            }
        }

        private string KeyToChar(Keys key, KeyboardState keyboardState)
        {
            bool shift = keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift);

            // Alfanumerické klávesy
            if (key >= Keys.A && key <= Keys.Z)
            {
                return shift ? key.ToString() : key.ToString().ToLower();
            }

            // Číselné klávesy
            if (key >= Keys.D0 && key <= Keys.D9)
            {
                if (shift)
                {
                    char[] shiftNumbers = { ')', '!', '@', '#', '$', '%', '^', '&', '*', '(' };
                    return shiftNumbers[key - Keys.D0].ToString();
                }
                return (key - Keys.D0).ToString();
            }

            // Numerická klávesnice
            if (key >= Keys.NumPad0 && key <= Keys.NumPad9)
            {
                return (key - Keys.NumPad0).ToString();
            }

            // Speciální znaky
            switch (key)
            {
                case Keys.Space: return " ";
                case Keys.OemMinus: return shift ? "_" : "-";
                case Keys.OemPlus: return shift ? "+" : "=";
                case Keys.OemOpenBrackets: return shift ? "{" : "[";
                case Keys.OemCloseBrackets: return shift ? "}" : "]";
                case Keys.OemPipe: return shift ? "|" : "\\";
                case Keys.OemSemicolon: return shift ? ":" : ";";
                case Keys.OemQuotes: return shift ? "\"" : "'";
                case Keys.OemComma: return shift ? "<" : ",";
                case Keys.OemPeriod: return shift ? ">" : ".";
                case Keys.OemQuestion: return shift ? "?" : "/";
                case Keys.Decimal: return ".";
                case Keys.Add: return "+";
                case Keys.Subtract: return "-";
                case Keys.Multiply: return "*";
                case Keys.Divide: return "/";
            }

            return string.Empty;
        }

        public void ScrollUp()
        {
            if (_scrollOffset < _history.Count - 10)
            {
                _scrollOffset++;
            }
        }

        public void ScrollDown()
        {
            if (_scrollOffset > 0)
            {
                _scrollOffset--;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Vykreslení pozadí konzole
            spriteBatch.Draw(_whiteTexture, _consoleRect, Color.Black * 0.7f);

            // Vykreslení historie
            int linesDisplayed = Math.Min(10, _history.Count - _scrollOffset);
            int startIndex = Math.Max(0, _history.Count - linesDisplayed - _scrollOffset);

            for (int i = 0; i < linesDisplayed; i++)
            {
                int historyIndex = startIndex + i;
                spriteBatch.DrawString(_font, 
                    _history[historyIndex], 
                    new Vector2(_consoleRect.X + 10, _consoleRect.Y + 10 + i * _font.LineSpacing), 
                    Color.White);
            }

            // Vykreslení aktuálního vstupu
            spriteBatch.DrawString(_font, 
                "> " + _currentInput + (DateTime.Now.Millisecond < 500 ? "_" : ""), 
                new Vector2(_consoleRect.X + 10, _consoleRect.Y + _consoleRect.Height - _font.LineSpacing - 10), 
                Color.White);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Button
{
    private Texture2D _texture;
    private SpriteFont _font;
    private Rectangle _rectangle;
    private string _text;
    private Color _buttonColor;
    private Color _textColor;

    private MouseState _currentMouseState;
    private MouseState _previousMouseState;

    public Button(Texture2D texture, SpriteFont font, Rectangle rectangle, string text, Color buttonColor, Color textColor)
    {
        _texture = texture;
        _font = font;
        _rectangle = rectangle;
        _text = text;
        _buttonColor = buttonColor;
        _textColor = textColor;
    }

    public bool IsClicked()
    {
        _previousMouseState = _currentMouseState;
        _currentMouseState = Mouse.GetState();

        return _previousMouseState.LeftButton == ButtonState.Pressed &&
               _currentMouseState.LeftButton == ButtonState.Released &&
               _rectangle.Contains(_currentMouseState.Position);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _rectangle, _buttonColor);
        Vector2 textSize = _font.MeasureString(_text);
        Vector2 textPosition = new Vector2(
            _rectangle.X + (_rectangle.Width - textSize.X) / 2,
            _rectangle.Y + (_rectangle.Height - textSize.Y) / 2
        );
        spriteBatch.DrawString(_font, _text, textPosition, _textColor);
    }
}

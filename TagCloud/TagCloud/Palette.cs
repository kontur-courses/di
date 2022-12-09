namespace TagCloud;

public class Palette
{
    public Palette(Color background, Color foreground)
    {
        Background = background;
        Foreground = foreground;
    }

    public Color Background { get; set; } = Color.White;
    public Color Foreground { get; set; } = Color.Black;
}
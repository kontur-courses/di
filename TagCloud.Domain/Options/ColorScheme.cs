using System.Drawing;

public class ColorScheme
{
    public static ColorScheme CalmScheme => new ColorScheme()
    {
        BackgroundColor = Color.FromArgb(255, 193, 217, 249),
        FontColor = Color.FromArgb(255, 90, 137, 185)
    };

    public static ColorScheme WarmScheme => new ColorScheme()
    {
        BackgroundColor = Color.FromArgb(255, 238, 212, 171),
        FontColor = Color.FromArgb(255, 176, 62, 2)
    };


    public Color BackgroundColor { get; set; }
    public Color FontColor { get; set; }
}
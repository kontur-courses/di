namespace TagCloudApp.Domain;

public class TagCloudPaintSettings
{
    public int MinFontSize { get; set; } = 10;
    public int MaxFontSize { get; set; } = 20;
    public Font BasicFont { get; set; } = new(FontFamily.GenericSansSerif, 1);
    public Color WordsColor { get; set; } = Color.Chocolate;
    public Color BackgroundColor { get; set; } = Color.Black;
}
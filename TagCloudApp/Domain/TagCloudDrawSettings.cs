namespace TagCloudApp.Domain;

public class TagCloudDrawSettings
{
    public int MinFontSize { get; set; } = 10;
    public int MaxFontSize { get; set; } = 20;
    public FontFamily FontFamily { get; set; } = FontFamily.GenericSansSerif;
    public Color WordsColor { get; set; } = Color.Chocolate;
    public Color BackgroundColor { get; set; } = Color.Black;
}
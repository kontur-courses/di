namespace TagCloud.Domain.Settings;

public class VisualizerSettings
{
    public Font Font { get; set; } = new (FontFamily.GenericMonospace, 14, FontStyle.Regular);
    public Color Color { get; set; } = Color.Black;
    public Color BgColor { get; set; } = Color.Chocolate;
}
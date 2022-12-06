using System.ComponentModel;
using System.Drawing;

namespace TagCloudCreator.Domain.Settings;

public class TagCloudPaintSettings
{
    [DisplayName("Min font size")]
    [Description("Min font size for words")]
    public int MinFontSize { get; set; } = 10;

    [DisplayName("Max font size")]
    [Description("Max font size for words")]
    public int MaxFontSize { get; set; } = 20;

    [DisplayName("Basic font")]
    [Description("Basic font settings for words, size does not matter")]
    public Font BasicFont { get; set; } = new(FontFamily.GenericSansSerif, 1);

    [DisplayName("Words color")]
    [Description("Words color")]
    public Color WordsColor { get; set; } = Color.Black;

    [DisplayName("Background color")]
    [Description("Background color")]
    public Color BackgroundColor { get; set; } = Color.Transparent;
}
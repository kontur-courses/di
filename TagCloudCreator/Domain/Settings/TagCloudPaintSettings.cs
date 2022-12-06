using System.ComponentModel;
using System.Drawing;

namespace TagCloudCreator.Domain.Settings;

public class TagCloudPaintSettings
{
    private int _minFontSize = 10;

    [DisplayName("Min font size")]
    [Description("Min font size for words")]
    public int MinFontSize
    {
        get => _minFontSize;
        set
        {
            if (value > MaxFontSize)
                throw new ArgumentException($"{nameof(MinFontSize)} cannot be greater than {nameof(MaxFontSize)}");
            _minFontSize = value;
        }
    }

    private int _maxFontSize = 20;

    [DisplayName("Max font size")]
    [Description("Max font size for words")]
    public int MaxFontSize
    {
        get => _maxFontSize;
        set
        {
            if (value < MinFontSize)
                throw new ArgumentException($"{nameof(MaxFontSize)} cannot be less than {nameof(MinFontSize)}");
            _maxFontSize = value;
        }
    }

    [DisplayName("Basic font")]
    [Description("Basic font settings for words, size does not matter")]
    public Font BasicFont { get; set; } = new(FontFamily.GenericSansSerif, 1);

    [DisplayName("Words color")]
    [Description("Words color")]
    public Color WordsColor { get; set; } = Color.White;

    [DisplayName("Background color")]
    [Description("Background color")]
    public Color BackgroundColor { get; set; } = Color.Transparent;
}
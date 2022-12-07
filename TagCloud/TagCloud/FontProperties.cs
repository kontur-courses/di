namespace TagCloud;

public class FontProperties
{
    public FontProperties(float minSize = 10f,
        float maxSize = 128f,
        FontStyle style = FontStyle.Regular,
        HorizontalAlignment textAlign = HorizontalAlignment.Center)
    {
        MinSize = minSize;
        MaxSize = maxSize;
        Style = style;
        TextAlign = textAlign;
    }

    public FontFamily Family { get; set; } = FontFamily.GenericSansSerif;
    public float MinSize { get; set; }
    public float MaxSize { get; set; }
    public FontStyle Style { get; set; }
    public HorizontalAlignment TextAlign { get; set; }
}
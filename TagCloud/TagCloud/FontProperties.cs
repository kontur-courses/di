namespace TagCloud;

public class FontProperties
{
    public FontProperties(float minSize = 10f,
        float maxSize = 64f,
        FontStyle style = FontStyle.Regular,
        ContentAlignment textAlign = ContentAlignment.MiddleCenter)
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
    public ContentAlignment TextAlign { get; set; }
}
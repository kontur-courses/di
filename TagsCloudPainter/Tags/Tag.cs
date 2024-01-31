namespace TagsCloudPainter.Tags;

public class Tag
{
    public Tag(string value, float fontSize, int count)
    {
        ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
        Value = value;
        FontSize = fontSize;
        Count = count;
    }

    public string Value { get; private set; }
    public float FontSize { get; private set; }
    public int Count { get; private set; }
}
namespace TagsCloudContainer.WordProcessing;

public class Word
{
    public int Count { get; set; }
    public float Size { get; set; }
    public string Value { get; }

    public Word(string value)
    {
        Value = value;
        Count = 1;
    }

    public void GenerateFontSize(Settings settings, int wordCount) =>
        Size = Count / (float)wordCount * 100 * settings.FontSize;
}
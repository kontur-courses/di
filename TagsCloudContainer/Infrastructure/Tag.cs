namespace TagsCloudContainer.Infrastructure;

public record Tag
{
    public double Frequency { get; init; }
    public string Text { get; init; }
    public WordType WordType { get; init; }

    public Tag(double frequency, string text, WordType wordType)
    {
        Frequency = frequency;
        Text = text;
        WordType = wordType;
    }
}

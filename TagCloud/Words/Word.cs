namespace TagCloud.Words;

public class Word
{
    public Word(string value, float frequency)
    {
        Value = value;
        Frequency = frequency;
    }

    public string Value { get; set; }
    public float Frequency { get; set; } = 0;
}
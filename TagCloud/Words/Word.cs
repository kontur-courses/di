namespace TagCloud.Words;

public class Word
{
    public Word(string value)
    {
        Value = value;
    }

    public string Value { get; set; }
    public int Amount { get; set; } = 0;
    public float Frequency { get; set; } = 0;
}
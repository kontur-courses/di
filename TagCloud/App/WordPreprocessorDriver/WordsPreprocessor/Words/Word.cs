namespace TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

public class Word : IWord
{
    public string Value { get; }
    public int Count { get; set; }
    public double Tf { get; set; }
    
    public Word(string value, int count = 1, double tf = 1)
    {
        Value = value;
        Count = count;
        Tf = tf;
    }

    public bool Equals(IWord? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
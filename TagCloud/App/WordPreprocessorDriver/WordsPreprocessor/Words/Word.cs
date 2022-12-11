using System.Drawing;

namespace TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

/// <summary>
/// Класс слова, который хранит информацию о слове, его количестве встреч в слове и индексе tf
/// </summary>
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
    
    public Size MeasureWord(Font font)
    {
        using var bitmap = new Bitmap(1, 1);
        using var graphics = Graphics.FromImage(bitmap);
        var result = graphics.MeasureString(Value, font);
        if (result.Width < 1) result.Width = 1;
        if (result.Height < 1) result.Height = 1;
        return result.ToSize();
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
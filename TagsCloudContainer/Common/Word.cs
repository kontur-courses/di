using System.Drawing;

namespace TagsCloudContainer.Common;

public class Word
{
    public string Value { get; }
    public Rectangle Rectangle { get; }
    public int Frequency { get; }

    public Word(string value, Rectangle rectangle, int frequency)
    {
        Value = value;
        Rectangle = rectangle;
        Frequency = frequency;
    }
}
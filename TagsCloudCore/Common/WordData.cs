using System.Drawing;

namespace TagsCloudCore.Common;

public class WordData
{
    public Rectangle Rectangle { get; }
    public int Frequency { get; }

    public WordData(Rectangle rectangle, int frequency)
    {
        Rectangle = rectangle;
        Frequency = frequency;
    }
}
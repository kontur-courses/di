using System.Drawing;

namespace TagCloud2.TextGeometry
{
    public interface IColoredSizedWord
    {
        Rectangle Size { get; }

        Color Color { get; }

        string Word { get; }

        Font Font { get; }
    }
}

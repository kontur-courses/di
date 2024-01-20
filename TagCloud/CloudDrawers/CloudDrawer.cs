using System.Drawing;
using TagCloud;

namespace TagCloudTests;

public interface ICloudDrawer
{
    int FontSize { get; }
    void Draw(IEnumerable<TextRectangle> rectangle);
    Size GetTextRectangleSize(string text, int size);
}
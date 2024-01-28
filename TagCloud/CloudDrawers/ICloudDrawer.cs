using System.Drawing;
using TagCloud;

namespace TagCloudTests;

public interface ICloudDrawer
{
    int FontSize { get; }
    void Draw(List<TextRectangle> rectangle);
    Size GetTextRectangleSize(string text, int size);
}
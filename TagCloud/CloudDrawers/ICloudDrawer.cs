using System.Drawing;
using TagCloud;

namespace TagCloudTests;

public interface ICloudDrawer
{
    void Draw(List<TextRectangle> rectangle);
}
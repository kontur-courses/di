using System.Drawing;

namespace TagCloudTests;

public interface ICloudDrawer
{
    void Draw(IEnumerable<Rectangle> rectangle, string name);
}
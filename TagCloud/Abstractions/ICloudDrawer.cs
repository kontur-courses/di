using System.Drawing;

namespace TagCloud.Abstractions;

public interface ICloudDrawer
{
    Bitmap DrawTagCloud(IEnumerable<(string, int, Rectangle)> words);
}
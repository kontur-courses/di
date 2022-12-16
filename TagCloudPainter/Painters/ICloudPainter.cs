using System.Drawing;
using TagCloudPainter.Common;

namespace TagCloudPainter.Painters;

public interface ICloudPainter
{
    Bitmap PaintTagCloud(IEnumerable<Tag> tags);
}
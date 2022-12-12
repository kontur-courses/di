using System.Drawing;
using TagCloudPainter.Common;

namespace TagCloudPainter.Interfaces;

public interface ICloudPainter
{
    Bitmap PaintTagCloud(IEnumerable<Tag> tags);
}
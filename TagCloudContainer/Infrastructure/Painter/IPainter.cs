using System.Drawing;

namespace TagCloud.Infrastructure.Painter;

public interface IPainter
{
    Bitmap CreateImage(Dictionary<string, int> weightedWords);
}
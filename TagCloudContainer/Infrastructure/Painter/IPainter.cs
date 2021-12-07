using System.Drawing;

namespace TagCloudContainer.Infrastructure.Painter;

public interface IPainter
{
    Bitmap CreateImage(Dictionary<string, int> weightedWords);
}
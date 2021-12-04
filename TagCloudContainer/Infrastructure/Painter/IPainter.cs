using System.Drawing;
using TagCloudContainer.Infrastructure.WordWeigher;

namespace TagCloudContainer.Infrastructure.Painter
{
    public interface IPainter
    {
        Bitmap CreateImage(IEnumerable<WeightedWord> weightedWords, int imageWidth, int imageHeight, string fontName);
    }
}

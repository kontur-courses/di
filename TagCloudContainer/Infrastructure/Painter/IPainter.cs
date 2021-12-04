using System.Drawing;
using TagCloudContainer.Infrastructure.WordWeigher;

namespace TagCloudContainer.Infrastructure.Painter;

public interface IPainter
{
    Bitmap CreateImage(ICollection<WeightedWord> weightedWords);
}
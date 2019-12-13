using System.Drawing;

namespace TagsCloudContainer.TagCloudVisualization
{
    public interface IPlacingFunc
    {
        Point CalculatePoint(double param);
    }
}
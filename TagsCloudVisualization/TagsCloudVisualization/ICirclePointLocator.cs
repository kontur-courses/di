using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ICirclePointLocator
    {
        Point Center { get; }
        Point GetNextPoint();

        double DistanceFromCenter { get; set; }
    }
}
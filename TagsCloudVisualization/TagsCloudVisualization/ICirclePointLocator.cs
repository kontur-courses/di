using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ICirclePointLocator
    {
        double DistanceFromCenter { get; set; }
        Point GetNextPoint();
    }
}
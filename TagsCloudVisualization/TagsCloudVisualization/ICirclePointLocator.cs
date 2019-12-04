using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ICirclePointLocator
    {
        Point GetNextPoint();

        double DistanceFromCenter { get; set; }
    }
}
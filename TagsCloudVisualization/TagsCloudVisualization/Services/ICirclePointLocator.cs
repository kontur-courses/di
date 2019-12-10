using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ICirclePointLocator
    {
        double Angle { get; set; }
        double DistanceFromCenter { get; set; }
        Point GetNextPoint();
    }
}
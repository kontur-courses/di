using System.Drawing;

namespace TagsCloudVisualization.Services
{
    public interface ICirclePointLocator
    {
        double Angle { get; set; }
        double DistanceFromCenter { get; set; }
        Point GetNextPoint();
    }
}
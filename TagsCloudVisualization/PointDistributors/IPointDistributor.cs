using System.Drawing;

namespace TagsCloudVisualization.PointDistributors
{
    public interface IPointDistributor
    {
        Point GetPosition();
        Point GetCenter();
    }
}
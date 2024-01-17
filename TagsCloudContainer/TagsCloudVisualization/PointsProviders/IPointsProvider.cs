using System.Drawing;

namespace TagsCloudVisualization.PointsProviders;

public interface IPointsProvider
{
    public Point Start { get; }
    public IEnumerable<Point> GetPoints();
}
using System.Drawing;

namespace TagsCloudVisualization.PointCreators;

public interface IPointCreator
{
    public IEnumerable<Point> GetPoints();
}

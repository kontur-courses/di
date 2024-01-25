using System.Drawing;

namespace TagsCloudVisualization;

public interface IPoints
{
    IEnumerable<Point> GetPoints();
}
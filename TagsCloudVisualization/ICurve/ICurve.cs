using System.Drawing;

namespace TagsCloudVisualization;

public interface ICurve
{
    public IEnumerable<Point> GetNextPoint();
}
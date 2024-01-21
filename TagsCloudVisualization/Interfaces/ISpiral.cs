using System.Drawing;

namespace TagsCloudVisualization.Interfaces;

public interface ISpiral
{
    public IEnumerable<Point> GetPointsOnSpiral();
}

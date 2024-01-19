using System.Drawing;

namespace TagCloud;

public interface ICloudShaper
{
    IEnumerable<Point> GetPossiblePoints();
}
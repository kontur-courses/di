using System.Drawing;

namespace TagCloud.CloudLayouter
{
    public interface IPointGenerator
    {
        Point GetNextPoint();
    }
}

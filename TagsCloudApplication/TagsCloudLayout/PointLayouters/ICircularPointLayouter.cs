using System.Drawing;

namespace TagsCloudLayout.PointLayouters
{
    public interface ICircularPointLayouter
    {
        Point Center { get; }

        Point CalculateNextPoint();
    }
}

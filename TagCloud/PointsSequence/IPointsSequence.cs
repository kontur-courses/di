using System.Drawing;

namespace TagCloud.PointsSequence
{
    public interface IPointsSequence
    {
        Point GetNextPoint();

        void Reset();

        void SetCenter(Point center);
    }
}
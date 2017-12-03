using System.Drawing;

namespace TagCloud
{
    public interface IPointComputer
    {
        Point GetNextPoint(double radiusStep, double angleStep);
    }
}
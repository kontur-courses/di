using System.Drawing;

namespace TagCloudMaker
{
    public interface IPointComputer
    {
        Point GetNextPoint(double radiusStep, double angleStep);
    }
}
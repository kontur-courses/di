using System.Drawing;

namespace TagCloud.Interfaces
{
    public interface IPointComputer
    {
        Result<Point> GetNextPoint(double firstParamDelta, double secondParamDelta);
    }
}
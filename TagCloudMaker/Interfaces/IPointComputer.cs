using System.Drawing;

namespace TagCloud.Interfaces
{
    public interface IPointComputer
    {
        Point GetNextPoint(double firstParamDelta, double secondParamDelta);
    }
}
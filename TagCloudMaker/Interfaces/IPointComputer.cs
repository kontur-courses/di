using System.Drawing;

namespace TagCloud
{
    public interface IPointComputer
    {
        Point GetNextPoint(double firstParamDelta, double secondParamDelta);
    }
}
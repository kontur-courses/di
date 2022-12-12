using System.Drawing;

namespace TagCloud.Curves;

public interface ICurve
{
    Point GetPoint(double t);
}
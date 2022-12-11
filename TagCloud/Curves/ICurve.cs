using System.Drawing;

namespace TagCloud.Curves;

public interface ICurve
{
    Point GetPoint(double t);

    public static ICurve GetByName(string name)
    {
        return name.ToLower() switch
        {
            ArchimedeanSpiral.Name => new ArchimedeanSpiral(),
            _ => throw new ArgumentException("There is no such algorithm")
        };
    }
}
using System.Drawing;

namespace TagsCloudContainer.MathFunctions
{
    public interface IMathFunction
    {
        MathFunctionType Type { get; }
        float GetValue(PointF firstPoint, PointF secondPoint, int x);
    }
}
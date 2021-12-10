using System.Drawing;
using TagsCloudContainer.DependencyInjection;

namespace TagsCloudContainer.MathFunctions
{
    public interface IMathFunction : IService<MathFunctionType>
    {
        float GetValue(PointF firstPoint, PointF secondPoint, int x);
    }
}
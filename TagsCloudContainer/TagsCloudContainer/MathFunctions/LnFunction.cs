using System;
using System.Drawing;
using TagsCloudContainer.DependencyInjection;

namespace TagsCloudContainer.MathFunctions
{
    public class LnFunction : IMathFunction
    {
        public MathFunctionType Type => MathFunctionType.Ln;

        private readonly Lazy<IServiceResolver<MathFunctionType, IMathFunction>> functionResolver;

        public LnFunction(
            Lazy<IServiceResolver<MathFunctionType, IMathFunction>> functionResolver)
        {
            this.functionResolver = functionResolver;
        }

        public float GetValue(PointF firstPoint, PointF secondPoint, int x) =>
            MathF.Log(
                functionResolver.Value
                    .GetService(MathFunctionType.Linear)
                    .GetValue(firstPoint, secondPoint, x));
    }
}
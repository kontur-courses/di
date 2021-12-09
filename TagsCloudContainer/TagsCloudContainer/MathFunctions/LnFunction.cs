using System;
using System.Drawing;

namespace TagsCloudContainer.MathFunctions
{
    public class LnFunction : IMathFunction
    {
        public MathFunctionType Type => MathFunctionType.Ln;

        private readonly Lazy<IMathFunctionResolver> functionResolver;

        public LnFunction(Lazy<IMathFunctionResolver> functionResolver)
        {
            this.functionResolver = functionResolver;
        }

        public float GetValue(PointF firstPoint, PointF secondPoint, int x) =>
            MathF.Log(
                functionResolver.Value
                    .GetFunction(MathFunctionType.Linear)
                    .GetValue(firstPoint, secondPoint, x));
    }
}
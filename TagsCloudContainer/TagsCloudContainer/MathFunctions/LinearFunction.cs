using System.Drawing;

namespace TagsCloudContainer.MathFunctions
{
    public class LinearFunction : IMathFunction
    {
        public MathFunctionType Type => MathFunctionType.Linear;

        public float GetValue(PointF firstPoint, PointF secondPoint, int x)
        {
            var (k, b) = GetLineCoefficients(firstPoint, secondPoint);
            var functionParameters = new LinearFunctionParameters(firstPoint, secondPoint, k, b);
            return GetValue(functionParameters, x);
        }

        private static (float K, float B) GetLineCoefficients(PointF startPoint, PointF endPoint)
        {
            var kСoefficient = (endPoint.Y - startPoint.Y) / (endPoint.X - startPoint.X);
            var bСoefficient = startPoint.Y - kСoefficient * startPoint.X;
            return (kСoefficient, bСoefficient);
        }

        private static float GetValue(LinearFunctionParameters parameters, int x)
        {
            var (k, b, first, second) = (parameters.K, parameters.B, parameters.FirstPoint, parameters.SecondPoint);
            var value = k * x + b;
            if (float.IsNaN(value))
                return (first.Y + second.Y) / 2;

            return value;
        }
    }
}
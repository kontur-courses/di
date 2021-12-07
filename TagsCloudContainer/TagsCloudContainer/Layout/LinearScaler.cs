using System.Drawing;

namespace TagsCloudContainer.Layout
{
    public class LinearScaler : IScaler
    {
        private readonly float k;
        private readonly float b;
        private readonly PointF start;
        private readonly PointF end;

        public LinearScaler(PointF start, PointF end)
        {
            (k, b) = GetLineCoefficients(start, end);
            this.start = start;
            this.end = end;
        }

        public float GetValue(int x)
        {
            var value = k * x + b;
            if (float.IsNaN(value))
                return (start.Y + end.Y) / 2;

            return value;
        }

        private static (float K, float B) GetLineCoefficients(PointF startPoint, PointF endPoint)
        {
            var kСoefficient = (endPoint.Y - startPoint.Y) / (endPoint.X - startPoint.X);
            var bСoefficient = startPoint.Y - kСoefficient * startPoint.X;
            return (kСoefficient, bСoefficient);
        }
    }
}
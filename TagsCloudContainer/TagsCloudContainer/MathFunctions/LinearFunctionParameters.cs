using System.Drawing;

namespace TagsCloudContainer.MathFunctions
{
    public class LinearFunctionParameters
    {
        public PointF FirstPoint { get; }
        public PointF SecondPoint { get; }

        public float K { get; }
        public float B { get; }

        public LinearFunctionParameters(PointF firstPoint, PointF secondPoint, float k, float b)
        {
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
            K = k;
            B = b;
        }
    }
}
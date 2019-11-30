using System;

namespace TagsCloudVisualization.Utils
{
    public static class PointConverter
    {
        public static (float x, float y) TransformPolarToCartesian(float r, float theta)
        {
            return ((float) (r * Math.Cos(theta)), (float) (r * Math.Sin(theta)));
        }

        public static (float r, float theta) TransformCartesianToPolar(float x, float y)
        {
            return ((float) Math.Sqrt(x * x + y * y), (float) Math.Atan2(y, x));
        }
    }
}
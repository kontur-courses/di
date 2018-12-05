using System.Drawing;

namespace TagsCloudVisualization
{
    public struct SpiralInfo
    {
        public readonly double RadiusStep;
        public readonly double AngleStep;
        public readonly Point Center;
        public const int MaxAngle = 360;

        public SpiralInfo(double radiusStep, double angleStep, Point center)
        {
            RadiusStep = radiusStep;
            AngleStep = angleStep;
            Center = center;
        }
    }
}

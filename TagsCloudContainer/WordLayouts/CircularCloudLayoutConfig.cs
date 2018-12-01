using System;
using System.Drawing;

namespace TagsCloudContainer.WordLayouts
{
    public class CircularCloudLayoutConfig
    {
        public PointF CenterPoint { get; }

        public double AngleDelta { get; }

        public CircularCloudLayoutConfig(PointF centerPoint, double angleDelta)
        {
            if (angleDelta.CompareTo(0) == 0)
            {
                throw new ArgumentException("angleDelta can't be zero", nameof(angleDelta));
            }

            CenterPoint = centerPoint;
            AngleDelta = angleDelta;
        }
    }
}
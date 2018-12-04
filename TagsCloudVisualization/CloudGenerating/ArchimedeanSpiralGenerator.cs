using System;
using System.Drawing;

namespace TagsCloudVisualization.CloudGenerating
{
    public class ArchimedeanSpiralGenerator : ISpiralGenerator
    {
        private float currentAngle;
        private readonly PointF center;
        private readonly float step;
        private readonly float angleDeltaInRadians;

        public ArchimedeanSpiralGenerator(PointF center, float step, float angleDeltaInRadians)
        {
            this.center = center;
            this.step = step;
            this.angleDeltaInRadians = angleDeltaInRadians;
            currentAngle = 0f;
        }

        public PointF GetNextPoint()
        { 
            var distance = currentAngle * step / (2 * Math.PI);
            var xCoordinate = center.X + distance * Math.Cos(currentAngle);
            var yCoordinate = center.Y + distance * Math.Sin(currentAngle);

            currentAngle += angleDeltaInRadians;
            return new PointF((float)xCoordinate, (float)yCoordinate);
        }
    }
}

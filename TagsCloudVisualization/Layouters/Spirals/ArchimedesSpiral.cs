using System;
using System.Collections.Generic;
using System.Drawing;
using PointConverter = TagsCloudVisualization.Utils.PointConverter;

namespace TagsCloudVisualization.Layouters.Spirals
{
    public class ArchimedesSpiral : ISpiral
    {
        private const float DeltaAngle = (float) (5 * Math.PI / 180);
        private const float Thickness = 1;
        private readonly PointF center;
        private readonly float deltaAngle;

        private readonly float thickness;

        public ArchimedesSpiral()
        {
            thickness = Thickness;
            deltaAngle = DeltaAngle;
            center = new PointF(0, 0);
        }

        public ArchimedesSpiral(PointF center)
        {
            thickness = Thickness;
            deltaAngle = DeltaAngle;
            this.center = center;
        }

        public ArchimedesSpiral(PointF center, float thickness)
        {
            this.thickness = thickness;
            this.center = center;
            deltaAngle = DeltaAngle;
        }

        public ArchimedesSpiral(PointF center, float thickness, float deltaAngle)
        {
            this.thickness = thickness;
            this.center = center;
            this.deltaAngle = deltaAngle;
        }

        public IEnumerable<PointF> GetSpiralPoints()
        {
            for (float theta = 0;; theta += deltaAngle)
            {
                var r = thickness * theta;
                float x, y;
                (x, y) = PointConverter.TransformPolarToCartesian(r, theta);
                x += center.X;
                y += center.Y;
                yield return new PointF(x, y);
            }
        }
    }
}
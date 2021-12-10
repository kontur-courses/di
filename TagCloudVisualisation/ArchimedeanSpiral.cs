using System;
using System.Drawing;

namespace TagCloudVisualisation
{
    public class ArchimedeanSpiral : ISpiral
    {
        private readonly double angleSpeed;
        private readonly double linearSpeed;

        private double currentAngle;
        private double currentRadius;
        private Point center;

        public ArchimedeanSpiral(Point center, double angleSpeed = 0.108, double linearSpeed = 0.032)
        {
            if (angleSpeed == 0 || linearSpeed == 0)
            {
                throw new ArgumentException("angleSpeed or linearSpeed should not be 0");
            }

            this.linearSpeed = linearSpeed;
            this.angleSpeed = angleSpeed;
            this.center = center;
        }

        public Point GetNextPoint()
        {
            CurrentPoint = GeometryHelper
                .ConvertFromPolarToDecartWithFlooring(currentAngle, currentRadius)
                .Displace(center.X, center.Y);
            currentRadius += linearSpeed;
            currentAngle += angleSpeed;
            return CurrentPoint;
        }

        private Point CurrentPoint { get; set; }
    }
}

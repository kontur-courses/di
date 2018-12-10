using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualisation
{
    public class ArchimedeanSpiral : IEnumerable<Point>
    {
        private readonly double coefficients;
        private readonly double step;
        private readonly Point center;

        public ArchimedeanSpiral(Point center, double coefficients, double step)
        {
            if (coefficients <= 0)
            {
                throw new ArgumentException("coefficients should be a positive number");
            }

            if (step <= 0)
            {
                throw new ArgumentException("step should be a positive number");
            }

            this.step = step;
            this.coefficients = coefficients;
            this.center = center;
        }

        private double GetValuePolar(double angle)
        {
            return coefficients * angle;
        }

        private Point PolarToDekart(double angle, double distance)
        {
            var x = distance * Math.Cos(angle);
            var y = distance * Math.Sin(angle);
            return new Point((int) x + center.X, (int) y + center.Y);
        }

        private IEnumerable<Point> GetIEnumerableDecart()
        {
            if (step <= 0)
                throw new ArgumentException("Step should be a positive number");
            var currentAngle = 0.0;
            while (true)
            {
                yield return PolarToDekart(currentAngle, GetValuePolar(currentAngle));
                currentAngle += step;
            }
        }


        public IEnumerator<Point> GetEnumerator()
        {
            return GetIEnumerableDecart().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
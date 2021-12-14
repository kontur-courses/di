using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Utils;

namespace TagCloud.Layout
{
    internal class ArchimedeanSpiral : ICurve
    {
        private readonly int _offsetX;
        private readonly int _offsetY;
        private readonly double _radius = 1;
        private double _phi;

        public ArchimedeanSpiral() : this(Point.Empty)
        {
        }

        public ArchimedeanSpiral(Point center)
        {
            _offsetX = center.X;
            _offsetY = center.Y;
        }

        public IEnumerable<Point> GetDiscretePoints(double deltaAngle = 0.01)
        {
            while (true)
            {
                var rho = _phi * _radius / (2 * Math.PI);
                var cartesian = CoordinatesConverter.ToCartesian(rho, _phi);
                var point = new Point(cartesian.X + _offsetX, cartesian.Y + _offsetY);

                _phi += deltaAngle;
                yield return point;
            }
        }

        public void Reset()
        {
            _phi = 0;
        }
    }
}
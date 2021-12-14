using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Utils;

namespace TagCloud.Layout
{
    internal class Rose : ICurve
    {
        private readonly int _offsetX;
        private readonly int _offsetY;
        private double _phi;
        private double _radius = 1;

        public Rose() : this(Point.Empty)
        {
        }

        public Rose(Point center)
        {
            _offsetX = center.X;
            _offsetY = center.Y;
        }

        public IEnumerable<Point> GetDiscretePoints(double deltaAngle = 0.01)
        {
            while (true)
            {
                var rho = _radius * Math.Sin(15 * _phi);
                var cartesian = CoordinatesConverter.ToCartesian(rho, _phi);
                var point = new Point(cartesian.X + _offsetX, cartesian.Y + _offsetY);

                _phi += deltaAngle;
                if (Math.Abs(_phi % Math.PI) < 0.1)
                    _radius++;
                yield return point;
            }
        }

        public void Reset()
        {
            _radius = 1;
            _phi = 0;
        }
    }
}
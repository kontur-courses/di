using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Utils;

namespace TagCloud.Layout
{
    internal class Rose : ICurve
    {
        private readonly CoordinatesConverter _converter;
        
        /// <summary>
        /// Смещение по X к заданному центру
        /// </summary>
        private readonly int _offsetX;

        /// <summary>
        /// Смещение по Y к заданному центру
        /// </summary>
        private readonly int _offsetY;
        
        /// <summary>
        /// Радиус витков
        /// </summary>
        private double _radius = 1;

        /// <summary>
        /// Текущий угол в радианах
        /// </summary>
        private double _phi;
        
        public Rose(CoordinatesConverter converter) : this(Point.Empty, converter)
        {
        }
        
        public Rose(Point center, CoordinatesConverter converter)
        {
            _converter = converter;
            _offsetX = center.X;
            _offsetY = center.Y;
        }

        public IEnumerable<Point> GetDiscretePoints(double deltaAngle = 0.01)
        {
            while (true)
            {
                var rho = _radius * Math.Sin(15 * _phi);
                var cartesian = _converter.ToCartesian(rho, _phi);
                var point = new Point(cartesian.X + _offsetX, cartesian.Y + _offsetY);

                _phi += deltaAngle;
                if (Math.Abs(_phi % Math.PI) < 0.1)
                    _radius++;
                yield return point;
            }
        }
    }
}
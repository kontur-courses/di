using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Utils;

namespace TagCloud.Layout
{

    internal class ArchimedeanSpiral : ICurve
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
        private readonly double _radius = 1;

        /// <summary>
        /// Текущий угол в радианах
        /// </summary>
        private double _phi = 0;
        
        
        /// <summary>
        /// Создает новый объект спирали с центром в точке (0,0)
        /// </summary>
        public ArchimedeanSpiral(CoordinatesConverter converter) : this(Point.Empty, converter)
        {
        }

        /// <summary>
        /// Создает новый объект спирали с центром в точке Point center
        /// </summary>
        /// <param name="center">Центр спирали</param>
        /// <param name="converter">Конвертер координат</param>
        public ArchimedeanSpiral(Point center, CoordinatesConverter converter)
        {
            _converter = converter;
            _offsetX = center.X;
            _offsetY = center.Y;
        }

        public IEnumerable<Point> GetDiscretePoints(double deltaAngle = 0.01)
        {
            while (true)
            {
                var rho = _phi * _radius / (2 * Math.PI);
                var cartesian = _converter.ToCartesian(rho, _phi);
                var point = new Point(cartesian.X + _offsetX, cartesian.Y + _offsetY);

                _phi += deltaAngle;
                yield return point;
            }
        }
    }
}

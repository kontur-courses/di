using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{

    public class ArchimedeanSpiral : ISpiral
    {
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
        public ArchimedeanSpiral() : this(Point.Empty)
        {
        }

        /// <summary>
        /// Создает новый объект спирали с центром в точке Point center
        /// </summary>
        /// <param name="center">Центр спирали</param>
        /// 
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
    }
}

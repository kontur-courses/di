using System;
using System.Drawing;

namespace TagsCloudContainer
{
    public class Spiral
    {
        private const double AngleStep = Math.PI / 10;
        private double _angle;
        private Point _prevPoint;
        private double _step;
        
        public Spiral(Point start, double step)
        {
            if (step <= 0)
                throw new ArgumentException("step must not be less than or equal to 0");
            _prevPoint = start;
            _step = step;
        }

        public Point NextPoint()
        {
            _angle += AngleStep;
            var rho = _angle * _step / (2 * Math.PI);
            var x = rho * Math.Cos(_angle) + _prevPoint.X;
            var y = rho * Math.Sin(_angle) + _prevPoint.Y;
            _prevPoint = new Point(Convert.ToInt32(x), Convert.ToInt32(y)); 
            return _prevPoint;
        }
    }
}
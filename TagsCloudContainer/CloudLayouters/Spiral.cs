using System;
using System.Drawing;

namespace TagsCloudContainer
{
    class Spiral
    {
        private double _radius;
        private double _angle;
        private readonly double _radiusStep;
        private const double AngleStep = 0.1;
        private Point _center;
        private const double RadiusStepCoefficient = 0.06;
        private const int ScaleDivider = 50;


        public Spiral(Point center)
        {
            _center = center;
            _radius = 0;
            _radiusStep = RadiusStepCoefficient * center.X / ScaleDivider;
        }

        public Point GetNextPosition(Size rectangleSize)
        {
            var position = new Point(
                (int)(_center.X + _radius * Math.Cos(_angle) - rectangleSize.Width / 2),
                (int)(_center.Y - _radius * Math.Sin(_angle)) - rectangleSize.Height / 2);
            _radius += _radiusStep;
            _angle += AngleStep;
            return position;
        }

        public void RollBackRadius(int pixelsValue)
        {
            _radius = Math.Max(0, _radius - pixelsValue);
        }
    }
}

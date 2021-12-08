using System;
using System.Drawing;

namespace TagsCloudVisualization.CloudLayouter.VectorsGenerator
{
    public class CircularVectorsGenerator : IVectorsGenerator
    {
        private readonly int _angles;
        private readonly double _multiplier;
        private double _lastAngle;

        public CircularVectorsGenerator(double multiplier, int angles)
        {
            _multiplier = multiplier;
            _angles = angles;
            _lastAngle = 0;
        }

        public Point GetNextVector()
        {
            var step = Math.PI * 2 / _angles;
            var x = Convert.ToInt32(_multiplier * _lastAngle * Math.Cos(_lastAngle));
            var y = Convert.ToInt32(_multiplier * _lastAngle * Math.Sin(_lastAngle));
            _lastAngle += step;
            return new Point(x, y);
        }
    }
}
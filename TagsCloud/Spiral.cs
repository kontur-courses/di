using System;
using System.Drawing;

namespace TagsCloudVisualization
{
	internal class Spiral
	{
		private double _currentAngle;
		private readonly Point _center;
		private readonly double _density;
		private readonly double _angleStep;

		public Spiral(double angleStep, double density, Point center)
		{
			_angleStep = angleStep;
			_density = density;
			_center = center;
		}

		public Point GetNextPoint()
		{
			var x = Math.Round(_density * _currentAngle * Math.Sin(_currentAngle)) + _center.X;
			var y = Math.Round(_density * _currentAngle * Math.Cos(_currentAngle)) + _center.Y;
			_currentAngle += _angleStep;
			return new Point((int) x, (int) y);
		}
	}
}
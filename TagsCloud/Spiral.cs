using System;
using System.Drawing;

namespace TagsCloud
{
	internal class Spiral
	{
		private double _currentAngle;
		private readonly double _density;
		private readonly double _angleStep;

		public Spiral(double angleStep, double density)
		{
			_angleStep = angleStep;
			_density = density;
		}

		public Point GetNextPoint()
		{
			var x = Math.Round(_density * _currentAngle * Math.Sin(_currentAngle));
			var y = Math.Round(_density * _currentAngle * Math.Cos(_currentAngle));
			_currentAngle += _angleStep;
			return new Point((int) x, (int) y);
		}
	}
}
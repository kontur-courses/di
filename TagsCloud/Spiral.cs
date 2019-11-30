using System;
using System.Drawing;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class ArchimedeSpiral: ISpiral
	{
		private double _currentAngle;
		private readonly double _density;
		private readonly double _angleStep;

		public ArchimedeSpiral(SpiralSettings settings)
		{
			_angleStep = settings.AngleStepDegrees;
			_density = Math.PI / 180 * settings.Density;
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
using System;
using System.Drawing;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class ArchimedeSpiral: ISpiral
	{
		private readonly SpiralSettings _settings;
		private double _currentAngle;

		public ArchimedeSpiral(SpiralSettings settings) => _settings = settings;

		public Point GetNextPoint()
		{
			var _density = Math.PI / 180 * _settings.Density;
			var x = Math.Round(_density * _currentAngle * Math.Sin(_currentAngle));
			var y = Math.Round(_density * _currentAngle * Math.Cos(_currentAngle));
			_currentAngle += _settings.AngleStepDegrees;
			return new Point((int) x, (int) y);
		}

		public void ResetState() => _currentAngle = 0;
	}
}
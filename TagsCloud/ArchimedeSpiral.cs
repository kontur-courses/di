using System;
using System.Drawing;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class ArchimedeSpiral: ISpiral
	{
		private readonly SpiralParameters parameters;
		private double currentAngle;

		public ArchimedeSpiral(SpiralParameters parameters) => this.parameters = parameters;

		public Point GetNextPoint()
		{
			var density = Math.PI / 180 * parameters.Density;
			var x = Math.Round(density * currentAngle * Math.Sin(currentAngle));
			var y = Math.Round(density * currentAngle * Math.Cos(currentAngle));
			currentAngle += parameters.AngleStepDegrees;
			return new Point((int) x, (int) y);
		}

		public void ResetState() => currentAngle = 0;
	}
}
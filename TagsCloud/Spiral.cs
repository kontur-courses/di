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
			FailIfParametersAreInvalid();
			
			var density = Math.PI / 180 * parameters.Density;
			var x = Math.Round(density * currentAngle * Math.Sin(currentAngle));
			var y = Math.Round(density * currentAngle * Math.Cos(currentAngle));
			currentAngle += parameters.AngleStepDegrees;
			return new Point((int) x, (int) y);
		}

		private void FailIfParametersAreInvalid()
		{
			if (Math.Abs(parameters.Density) < double.Epsilon || 
			    Math.Abs(parameters.AngleStepDegrees) < double.Epsilon)
				throw new ArgumentException("Spiral parameters can't be zero");
		}

		public void ResetState() => currentAngle = 0;
	}
}
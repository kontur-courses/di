using System;
using System.Drawing;

namespace TagsCloudVisualization.Curves
{
	public class Spiral : ICurve
	{
		private readonly double degreeStep;
		private readonly double factorStep;
		private int nextPointCounter;

		public Spiral(double factorStep, double degreeStep)
		{
			this.factorStep = factorStep;
			this.degreeStep = degreeStep;
			nextPointCounter = 0;
		}

		public Point GetNextPoint()
		{
			var degree = degreeStep * nextPointCounter;
			var factor = factorStep * nextPointCounter;
			var x = (int)(factor * Math.Sin(degree));
			var y = (int)(factor * Math.Cos(degree));
			nextPointCounter++;

			return new Point(x, y);
		}
	}
}
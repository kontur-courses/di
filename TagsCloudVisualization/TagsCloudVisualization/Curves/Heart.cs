using System;
using System.Drawing;

namespace TagsCloudVisualization.Curves
{
	public class Heart : ICurve
	{
		private readonly double degreeStep;
		private readonly double factorStep;
		private int nextPointCounter;

		public Heart(double factorStep, double degreeStep)
		{
			this.factorStep = factorStep;
			this.degreeStep = degreeStep;
			nextPointCounter = 0;
		}

		public Point GetNextPoint()
		{
			var degree = degreeStep * nextPointCounter;
			var factor = factorStep * nextPointCounter;
			var x = (int) (1.3 * factor * Math.Cos(degree));
			var y = (int) (-factor * (Math.Sin(degree) + Math.Sqrt(Math.Abs(Math.Cos(degree)))));
			nextPointCounter++;

			return new Point(x, y);
		}
	}
}
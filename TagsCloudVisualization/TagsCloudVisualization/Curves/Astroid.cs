using System;
using System.Drawing;

namespace TagsCloudVisualization.Curves
{
	public class Astroid : ICurve
	{
		private readonly double degreeStep;
		private readonly double factorStep;
		private int nextPointCounter;

		public Astroid(double factorStep, double degreeStep)
		{
			this.factorStep = factorStep;
			this.degreeStep = degreeStep;
			nextPointCounter = 0;
		}

		public Point GetNextPoint()
		{
			var degree = degreeStep * nextPointCounter;
			var factor = factorStep * nextPointCounter;
			var x = (int)(factor * Math.Pow(Math.Cos(degree), 3));
			var y = (int)(factor * Math.Pow(Math.Sin(degree), 3));
			nextPointCounter++;

			return new Point(x, y);
		}
	}
}
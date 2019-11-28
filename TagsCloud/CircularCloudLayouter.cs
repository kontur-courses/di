using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace TagsCloudVisualization
{
	public class CircularCloudLayouter
	{
		private const double Density = 1;
		private const double AngleStep = Math.PI / 180 * 1;

		private readonly Point _center;
		private readonly Spiral _spiral;

		public List<Rectangle> Rectangles { get; } = new List<Rectangle>();

		public CircularCloudLayouter(Point center)
		{
			_center = center;
			_spiral = new Spiral(AngleStep, Density, _center);
		}

		public CircularCloudLayouter() => _spiral = new Spiral(AngleStep, Density, _center);

		public Rectangle PutNextRectangle(Size rectangleSize)
		{
			if (rectangleSize.Width < 0 || rectangleSize.Height < 0)
				throw new ArgumentException("Size values must be non-negative");

			var newRectangle = new Rectangle(_spiral.GetNextPoint(), rectangleSize);
			while (Rectangles.Any(rect => newRectangle.Intersects(rect)))
				newRectangle = new Rectangle(_spiral.GetNextPoint(), rectangleSize);
			
			Rectangles.Add(newRectangle);
			return newRectangle;
		}
	}
}
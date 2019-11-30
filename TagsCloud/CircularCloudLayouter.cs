using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class CircularCloudLayouter: ICloudLayouter
	{
		private readonly ISpiral _spiral;

		public List<Rectangle> Rectangles { get; } = new List<Rectangle>();

		public CircularCloudLayouter(ISpiral spiral) => _spiral = spiral;

		public Rectangle PlaceNextRectangle(Size rectangleSize)
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
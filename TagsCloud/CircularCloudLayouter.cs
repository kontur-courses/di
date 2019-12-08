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
		private readonly List<Rectangle> _rectangles = new List<Rectangle>();

		public CircularCloudLayouter(ISpiral spiral) => _spiral = spiral;

		public Rectangle PlaceNextRectangle(Size rectangleSize)
		{
			if (rectangleSize.Width < 0 || rectangleSize.Height < 0)
				throw new ArgumentException("Size values must be non-negative");

			var newRectangle = new Rectangle(_spiral.GetNextPoint(), rectangleSize);
			while (_rectangles.Any(rect => newRectangle.Intersects(rect)))
				newRectangle = new Rectangle(_spiral.GetNextPoint(), rectangleSize);
			
			_rectangles.Add(newRectangle);
			return newRectangle;
		}

		public void ResetState()
		{
			_rectangles.Clear();
			_spiral.ResetState();
		}
	}
}
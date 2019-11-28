using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System;

namespace TagsCloudVisualization
{
	public static class RectanglesGenerator
	{
		public static IEnumerable<Rectangle> GenerateRectangles(int rectanglesCount, Size maxSize, Size minSize)
		{
			var sizes = GenerateSizes(rectanglesCount, maxSize, minSize);
			var layouter = new CircularCloudLayouter();
			var rectangles = sizes.Select(layouter.PutNextRectangle);
			return rectangles;
		}

		private static IEnumerable<Size> GenerateSizes(int count, Size maxSize, Size minSize)
		{
			var random = new Random();
			for (var i = 0; i < count; i++)
			{
				var height = random.Next(maxSize.Height, maxSize.Height);
				var width = random.Next(Math.Max(minSize.Width, height), maxSize.Width);
				yield return new Size(width, height);
			}
		}
	}
}
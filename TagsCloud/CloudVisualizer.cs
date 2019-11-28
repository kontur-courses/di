using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System;

namespace TagsCloudVisualization
{
	public static class CloudVisualizer
	{
		internal const int RectangleBorderWidth = 1;

		private static readonly Color[] _possibleRectangleColors = 
		{
			Color.DarkBlue,
			Color.DarkGreen,
			Color.Purple,
			Color.DarkRed,
			Color.DarkMagenta,
			Color.DarkTurquoise,
			Color.Red,
			Color.DarkOrange,
		};

		public static Image Visualize(Rectangle[] rectangles, bool forTest=false)
		{
			var imageSize = CalculateImageSize(rectangles);
			var movedRectangles = rectangles.Select(r => MoveToImageCenter(r, imageSize));
			
			var image = new Bitmap(imageSize.Width, imageSize.Height);
			if (forTest)
				image = DrawDiagonals(image);
			image = DrawRectangles(image, movedRectangles, forTest);
			return image;
		}

		internal static Size CalculateImageSize(IEnumerable<Rectangle> rectangles)
		{
			int maxX = int.MinValue, maxY = int.MinValue;
			foreach (var rectangle in rectangles)
			{
				maxX = GetAbsoluteMax(rectangle.Left, rectangle.Right, maxX);
				maxY = GetAbsoluteMax(rectangle.Top, rectangle.GetBottom(), maxY);
			}

			var width = maxX * 2 + RectangleBorderWidth;
			var height = maxY * 2 + RectangleBorderWidth;
			return new Size(width, height);
		}
		
		internal static Rectangle MoveToImageCenter(Rectangle rectangle, Size imageSize)
		{
			var xOffset = imageSize.Width / 2;
			var yOffset = -2 * rectangle.Y + imageSize.Height / 2;
			rectangle.Offset(xOffset, yOffset);
			return rectangle;
		}

		private static Bitmap DrawDiagonals(Bitmap image)
		{
			var graphics = Graphics.FromImage(image);
			var lineColor = Pens.Red;
			
			graphics.DrawLine(lineColor, 0, 0, image.Width, image.Height);
			graphics.DrawLine(lineColor, 0, image.Height, image.Width, 0);
			return image;
		}

		private static Bitmap DrawRectangles(Bitmap image, IEnumerable<Rectangle> rectangles, bool drawIndexes)
		{
			var graphics = Graphics.FromImage(image);
			var random = new Random();
			var index = 1;
			foreach (var rectangle in rectangles)
			{
				graphics.DrawRectangle(new Pen(GenerateColor(random), RectangleBorderWidth), rectangle);
				if (!drawIndexes) continue;
				var font = new Font(FontFamily.GenericMonospace, rectangle.Height / 2);
				graphics.DrawString($"{index++}", font, Brushes.Black, rectangle);
			}
			return image;
		}

		private static Color GenerateColor(Random random)
		{
			var colorIndex = random.Next(0, _possibleRectangleColors.Length);
			return _possibleRectangleColors[colorIndex];
		}

		internal static int GetAbsoluteMax(int number1, int number2, int previousMax) =>
			Math.Max(Math.Max(Math.Abs(number1), Math.Abs(number2)), previousMax);
	}
}
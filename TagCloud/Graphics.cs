using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloud
{
	internal class Graphics
	{
		private readonly Random Random = new Random();

		public void SaveMap(IReadOnlyCollection<Rectangle> allRectangles, string nameOfImage)
		{
			if (allRectangles.Count == 0)
				return;

			var boundingCoordinate = new BoundingCoordinate(allRectangles);

			using (var map = new Bitmap(boundingCoordinate.SizeX, boundingCoordinate.SizeY))
			using (var graphics = System.Drawing.Graphics.FromImage(map))
			{
				foreach (var rectangle in allRectangles)
				{
					var color = Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256));
					using (var brush = new SolidBrush(color))
					{
						var location = rectangle.Location - new Size(boundingCoordinate.MinX, boundingCoordinate.MinY);
						graphics.FillRectangle(brush, new Rectangle(location, rectangle.Size));
					}
				}
				map.Save($"{nameOfImage}.png", ImageFormat.Png);
			}
		}
	}
}
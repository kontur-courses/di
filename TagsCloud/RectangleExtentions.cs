﻿using System;
using System.Drawing;

namespace TagsCloud
{
	public static class RectangleExtensions
	{
		public static PointF GetCenter(this Rectangle rectangle)
		{
			var centerX = rectangle.X + rectangle.Width / 2f;
			var centerY = rectangle.Y - rectangle.Height / 2f;
			return new PointF(centerX, centerY);
		}

		public static bool Intersects(this Rectangle rectangle, Rectangle otherRectangle) =>
			!(rectangle.Left >= otherRectangle.Right || rectangle.Right <= otherRectangle.Left ||
			 rectangle.GetBottom() >= otherRectangle.Top || rectangle.Top <= otherRectangle.GetBottom());

		private static int GetBottom(this Rectangle rectangle) => rectangle.Y - rectangle.Height;
	}
}
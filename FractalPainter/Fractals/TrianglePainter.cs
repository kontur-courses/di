using System;
using System.Drawing;
using FractalPainting.Infrastructure;

namespace FractalPainting.Fractals
{
	public class TrianglePainter
	{
		private readonly IImageHolder imageHolder;
		private Size imageSize;

		public TrianglePainter(IImageHolder imageHolder)
		{
			this.imageHolder = imageHolder;
			imageSize = imageHolder.GetImageSize();
		}

		public void Paint()
		{
			using (var graphics = imageHolder.StartDrawing())
			{
				graphics.FillRectangle(Brushes.Black, 0, 0, imageSize.Width, imageSize.Height);
				DrawSegment(graphics, 0, imageSize.Height*0.9f, imageSize.Width, imageSize.Height * 0.9f);
			}
			imageHolder.UpdateUi();
		}

		private void DrawSegment(Graphics graphics, float x0, float y0, float x1, float y1)
		{
			var len2 = (x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1);
			if (len2 < 4)
			{
				if (y0 < 0 || y1 < 0) return;
				graphics.DrawLine(Pens.Yellow, x0, y0, x1, y1);
			}
			else
			{
				var vx = (x1 - x0) / 3;
				var vy = (y1 - y0) / 3;
				DrawSegment(graphics, x0, y0, x0 + vx, y0 + vy);
				float k = (float)Math.Sqrt(3) / 2f;
				var px = (x0 + x1) / 2 + vy * k;
				var py = (y0 + y1) / 2 - vx * k;
				DrawSegment(graphics, x0 + vx, y0 + vy, px, py);
				DrawSegment(graphics, px, py, x0 + 2 * vx, y0 + 2 * vy);
				DrawSegment(graphics, x0 + 2 * vx, y0 + 2 * vy, x1, y1);
			}
		}
	}
}
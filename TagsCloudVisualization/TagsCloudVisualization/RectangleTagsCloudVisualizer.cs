using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
	public static class RectangleTagsCloudVisualizer
	{
		private const int EdgeLength = 100;
		public static Bitmap GetPicture(Rectangle[] rectangles, Color color)
		{
			var width = rectangles.Max(r => r.Right) - rectangles.Min(r => r.Left);
			var height = rectangles.Max(r => r.Bottom) - rectangles.Min(r => r.Top);
			var bitmap = new Bitmap(width + EdgeLength, height + EdgeLength);
			var pen = new Pen(color, 1);

			using (var graphics = Graphics.FromImage(bitmap))
			{
				graphics.Clear(Color.Transparent);
				var horizontalOffset = (float) (width + EdgeLength) / 2;
				var verticalOffset = (float) height / 2 + (float) EdgeLength * 3 / 4;
				graphics.TranslateTransform(horizontalOffset, verticalOffset);
				graphics.DrawRectangles(pen, rectangles);
			}

			return bitmap;
		}
	}
}
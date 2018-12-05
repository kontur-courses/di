using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloud
{
	public class BoundingCoordinate
	{
		private readonly IReadOnlyCollection<Rectangle> allRectangles;
		public BoundingCoordinate(IReadOnlyCollection<Rectangle> allRectangles)
		{
			this.allRectangles = allRectangles;
		}

		public int MaxX => allRectangles.Max(f => f.X + f.Width);
		public int MaxY => allRectangles.Max(f => f.Y + f.Height);
		public int MinX => allRectangles.Min(f => f.X);
		public int MinY => allRectangles.Min(f => f.Y);

		public int SizeX => MaxX - MinX;
		public int SizeY => MaxY - MinY;
	}
}
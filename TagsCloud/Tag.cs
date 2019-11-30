using System.Drawing;

namespace TagsCloud
{
	public struct Tag
	{
		public string Text { get; }
		public int TextSize { get; }
		public Rectangle Area { get; }

		public Tag(string text, int textSize, Rectangle area)
		{
			Text = text;
			TextSize = textSize;
			Area = area;
		}
	}
}
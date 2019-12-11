using System.Drawing;

namespace TagsCloud
{
	public class FontSettings
	{
		public Font Font { get; set; } = new Font(FontFamily.GenericMonospace, 20);
		public int MaxFontSize { get; set; } = 60;
		public int MinFontSize { get; set; } = 10;
	}
}
namespace TagsCloud
{
	public class PainterSettings
	{
		public bool DrawWordRectangle { get; set; } = true;
		public ImageSettings ImageSettings { get; set; }
		public FontSettings Font { get; set; }
		public Palette Palette { get; set; }

		public PainterSettings(ImageSettings imageSettings, FontSettings fontSettings, Palette palette)
		{
			ImageSettings = imageSettings;
			Font = fontSettings;
			Palette = palette;
		}
	}
}
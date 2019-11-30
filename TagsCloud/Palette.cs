using System.Drawing;

namespace TagsCloud
{
	public class Palette
	{
		public Color TextColor { get; set; } = Color.Red;
		public Color BackgroundColor { get; set; } = Color.Black;
		public bool RandomizeColors { get; set; } = true;
		public Color[] PossibleTextColors { get; set; } = 
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
	}
}
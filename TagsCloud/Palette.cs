using System;
using System.Drawing;

namespace TagsCloud
{
	public class Palette
	{
		private readonly Random _randomizer;
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

		public Palette(Random randomizer) => _randomizer = randomizer;

		public Color GenerateColor()
		{
			var colorIndex = _randomizer.Next(0, PossibleTextColors.Length);
			return PossibleTextColors[colorIndex];
		}
	}
}
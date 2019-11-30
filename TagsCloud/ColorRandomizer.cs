using System;
using System.Drawing;

namespace TagsCloud
{
	public class ColorRandomizer
	{
		private readonly Palette _palette;
		private readonly Random _random;

		public ColorRandomizer(Palette palette, Random random)
		{
			this._palette = palette;
			this._random = random;
		}

		public Color GenerateColor()
		{
			var colorIndex = _random.Next(0, _palette.PossibleTextColors.Length);
			return _palette.PossibleTextColors[colorIndex];
		}
	}
}
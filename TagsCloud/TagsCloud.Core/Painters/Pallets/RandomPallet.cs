using System.Drawing;

namespace TagsCloud.Core.Painters.Pallets;

public class RandomPallet : ITagCLoudPallet
{
	private readonly Random random;

	public RandomPallet(Color backgroundColor)
	{
		BackgroundColor = backgroundColor;
		random = new Random();
	}

	public Color BackgroundColor { get; }

	public Color GetNextColor()
	{
		return Color.FromArgb(random.Next(50, 200), random.Next(50, 200), random.Next(50, 200));
	}
}
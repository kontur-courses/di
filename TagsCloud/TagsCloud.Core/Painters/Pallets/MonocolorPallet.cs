using System.Drawing;

namespace TagsCloud.Core.Painters.Pallets;

public class MonocolorPallet : ITagCLoudPallet
{
	private readonly Color fontColor;

	public MonocolorPallet(Color fontColor, Color backgroundColor)
	{
		this.fontColor = fontColor;
		BackgroundColor = backgroundColor;
	}

	public MonocolorPallet() : this(Color.Chartreuse, Color.DarkGray)
	{
	}

	public MonocolorPallet(Color fontColor) : this(fontColor, Color.DarkGray)
	{
	}

	public Color BackgroundColor { get; }

	public Color GetNextColor()
	{
		return fontColor;
	}
}
using System.Drawing;

namespace TagsCloud.Core.Painters.Pallets;

public class MonocolorPallet : ITagCLoudPallet
{
	public Color BackgroundColor => Color.DarkGray;

	public Color GetNextColor()
	{
		return Color.Chartreuse;
	}
}
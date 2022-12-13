using System.Drawing;

namespace TagsCloud.Core.TagContainersProviders;

public class TagContainer
{
	public TagContainer(Tag tag, Rectangle border, int fontSize)
	{
		Tag = tag;
		Border = border;
		FontSize = fontSize;
	}

	public Tag Tag { get; }

	public Rectangle Border { get; }

	public int FontSize { get; }

	public override string ToString()
	{
		return Tag.ToString();
	}
}
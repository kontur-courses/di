using System.Drawing;

namespace TagsCloud.Core.TagContainersCreators;

public class TagContainer
{
	public TagContainer(Tag tag, Rectangle border)
	{
		Tag = tag;
		Border = border;
	}

	public Tag Tag { get; }
	public Rectangle Border { get; }
}
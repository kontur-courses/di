using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class LayoutConstructor: ILayoutConstructor
	{
		private readonly IEnumerable<Tag> _sourceTags;
		private readonly ICloudLayouter _layouter;
		private readonly List<Tag> _layoutTags = new List<Tag>();

		public LayoutConstructor(TagsProcessor tagsProcessor, ICloudLayouter layouter)
		{
			_sourceTags = tagsProcessor.GetTags();
			_layouter = layouter;
		}

		public Layout GetLayout()
		{
			foreach (var tag in _sourceTags)
			{
				var tagArea = _layouter.PlaceNextRectangle(tag.Area.Size);
				_layoutTags.Add(new Tag(tag.Text, tag.TextSize, tagArea));
			}
			return new Layout(_layoutTags);
		}
	}
}
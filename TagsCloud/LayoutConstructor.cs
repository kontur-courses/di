using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class LayoutConstructor: ILayoutConstructor
	{
		private readonly ITagsProcessor _tagsProcessor;
		private readonly ICloudLayouter _layouter;
		private readonly List<Tag> _layoutTags;

		public LayoutConstructor(ITagsProcessor tagsProcessor, ICloudLayouter layouter)
		{
			_tagsProcessor = tagsProcessor;
			_layouter = layouter;
			_layoutTags = new List<Tag>();
		}

		public Layout GetLayout()
		{
			foreach (var tag in _tagsProcessor.GetTags())
			{
				var tagArea = _layouter.PlaceNextRectangle(tag.Area.Size);
				_layoutTags.Add(new Tag(tag.Text, tag.TextSize, tagArea));
			}
			return new Layout(_layoutTags);
		}
	}
}
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

		public LayoutConstructor(ITagsProcessor tagsProcessor, ICloudLayouter layouter)
		{
			_tagsProcessor = tagsProcessor;
			_layouter = layouter;
		}

		public Layout GetLayout()
		{
			var layoutTags = new List<Tag>();
			_layouter.ResetState();
			
			foreach (var tag in _tagsProcessor.GetTags())
			{
				var tagArea = _layouter.PlaceNextRectangle(tag.Area.Size);
				layoutTags.Add(new Tag(tag.Text, tag.TextSize, tagArea));
			}
			return new Layout(layoutTags);
		}
	}
}
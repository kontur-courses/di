using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class LayoutConstructor: ILayoutConstructor
	{
		private readonly ITagsProcessor tagsProcessor;
		private readonly ICloudLayouter layouter;

		public LayoutConstructor(ITagsProcessor tagsProcessor, ICloudLayouter layouter)
		{
			this.tagsProcessor = tagsProcessor;
			this.layouter = layouter;
		}

		public Layout GetLayout()
		{
			layouter.ResetState();
			var layoutTags = from tag in tagsProcessor.GetTags()
				let tagArea = layouter.PlaceNextRectangle(tag.Area.Size)
				select new Tag(tag.Text, tag.TextSize, tagArea);
			return new Layout(layoutTags);
		}
	}
}
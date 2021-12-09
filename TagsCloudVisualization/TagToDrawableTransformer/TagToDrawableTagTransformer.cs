using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.ColorService;
using TagsCloudVisualization.Drawable;
using TagsCloudVisualization.FontService;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.SizeService;

namespace TagsCloudVisualization.TagToDrawableTransformer
{
    public class TagToDrawableTagTransformer : ITagToDrawableTransformer
    {
        private readonly ILayouter layouter;
        private readonly ITagColorService tagColorService;
        private readonly ITagFontService tagFontService;
        private readonly ITagSizeService tagSizeService;

        public TagToDrawableTagTransformer(ITagFontService tagFontService, ITagSizeService tagSizeService,
            ITagColorService tagColorService, ILayouter layouter)
        {
            this.tagFontService = tagFontService;
            this.tagSizeService = tagSizeService;
            this.tagColorService = tagColorService;
            this.layouter = layouter;
        }

        public IEnumerable<IDrawable> Transform(List<Tag> tags)
        {
            var (min, max) = GetMinAndMaxCount(tags);
            return tags
                .OrderByDescending(x => x.Count)
                .Select(tag => CreateDrawableTag(tag, min, max));
        }


        private DrawableTag CreateDrawableTag(Tag tag, float min, float max)
        {
            var font = tagFontService.GetFont(tag, min, max);
            var size = tagSizeService.GetSize(tag, font);
            var bounds = layouter.PutNextRectangle(size);
            var color = tagColorService.GetColor();
            return new DrawableTag(tag, bounds, font, color);
        }

        private (float, float) GetMinAndMaxCount(List<Tag> tags)
        {
            var min = float.MaxValue;
            var max = float.MinValue;
            foreach (var tag in tags)
            {
                min = Math.Min(min, tag.Count);
                max = Math.Max(max, tag.Count);
            }

            return (min, max);
        }
    }
}
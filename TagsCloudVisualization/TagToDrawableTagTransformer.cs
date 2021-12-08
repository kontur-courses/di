using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.ColorService;
using TagsCloudVisualization.Drawable;
using TagsCloudVisualization.FontService;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.SizeService;

namespace TagsCloudVisualization
{
    public class TagToDrawableTagTransformer
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

        public IEnumerable<IDrawable> Transform(IEnumerable<Tag> tags)
        {
            var min = tags.Min(tag => tag.Count);
            var max = tags.Max(tag => tag.Count);
            var drawables = new List<IDrawable>();
            foreach (var tag in tags.OrderByDescending(x => x.Count))
            {
                var font = tagFontService.GetFont(tag, min, max);
                var size = tagSizeService.GetSize(tag, font);
                var bounds = layouter.PutNextRectangle(size);
                var color = tagColorService.GetColor();
                drawables.Add(new DrawableTag(tag, bounds, font, color));
            }

            return drawables;
        }
    }
}
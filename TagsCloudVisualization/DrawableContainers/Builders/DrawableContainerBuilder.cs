using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.ColorService;
using TagsCloudVisualization.Drawable;
using TagsCloudVisualization.FontService;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.SizeService;

namespace TagsCloudVisualization.DrawableContainers.Builders
{
    internal class DrawableContainerBuilder : IDrawableContainerBuilder
    {
        private readonly ITagFontService fontService;
        private readonly ITagColorService colorService;
        private readonly ITagSizeService sizeService;
        private readonly ILayouter layouter;
        private readonly List<Tag> tags = new();
        private float max = float.MinValue;
        private float min = float.MaxValue;

        public DrawableContainerBuilder(ITagFontService fontService, ITagColorService colorService,
            ITagSizeService sizeService, ILayouter layouter)
        {
            this.fontService = fontService;
            this.colorService = colorService;
            this.sizeService = sizeService;
            this.layouter = layouter;
        }

        public void AddTag(Tag tag)
        {
            max = Math.Max(max, tag.Count);
            min = Math.Min(min, tag.Count);
            tags.Add(tag);
        }

        public IDrawableContainer Build()
        {
            var container = new DrawableContainer();
            foreach (var tag in tags.OrderByDescending(tag => tag.Count))
            {
                container.AddDrawable(CreateDrawableTag(tag));
            }

            return container;
        }
        
        private DrawableTag CreateDrawableTag(Tag tag)
        {
            var font = fontService.GetFont(tag, min, max);
            var size = sizeService.GetSize(tag, font);
            var bounds = layouter.PutNextRectangle(size);
            var color = colorService.GetColor();
            return new DrawableTag(tag, bounds, font, color);
        }
    }
}
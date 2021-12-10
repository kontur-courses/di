using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.Painting
{
    public class TagPainter
    {
        private readonly IPalettesMaker maker;

        public TagPainter(IPalettesMaker maker)
        {
            this.maker = maker;
        }

        public List<SimpleTag> Paint(List<SimpleTag> tags)
        {
            foreach (var (tag, palette) in tags.Zip(maker.GetPalettes()))
                tag.Palette = palette;
            return tags;
        }
    }
}
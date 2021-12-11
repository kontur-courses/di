using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.Painting
{
    public class TagPainter
    {
        private readonly IPalettesMaker maker;

        public TagPainter(IPalettesMaker maker)
        {
            this.maker = maker;
        }

        public void SetPalettes(IReadOnlyList<ITag> tags)
        {
            foreach (var (tag, palette) in tags.Zip(maker.GetPalettes()))
                tag.Palette = palette;
        }
    }
}
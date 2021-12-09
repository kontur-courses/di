using System;
using System.Collections.Generic;
using TagCloudContainerTests;

namespace TagsCloudContainer
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
            var enumerator = maker.GetPalettes().GetEnumerator();
            foreach (var simpleTag in tags)
            {
                if (!enumerator.MoveNext())
                    throw new ArgumentException("Palettes should not ends before tags");
                var palette = enumerator.Current;
                simpleTag.Palette = palette;
            }
            return tags;
        }
    }
}
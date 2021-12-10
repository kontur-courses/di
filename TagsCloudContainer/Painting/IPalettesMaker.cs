using System.Collections.Generic;
using TagsCloudVisualization;

namespace TagsCloudContainer.Painting
{
    public interface IPalettesMaker
    {
        IEnumerable<Palette> GetPalettes();
    }
}
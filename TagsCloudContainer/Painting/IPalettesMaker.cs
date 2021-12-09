using System.Collections.Generic;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public interface IPalettesMaker
    {
        IEnumerable<Palette> GetPalettes();
    }
}
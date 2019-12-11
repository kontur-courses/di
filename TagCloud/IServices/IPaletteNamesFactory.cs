using System.Collections.Generic;

namespace TagCloud.IServices
{
    public interface IPaletteNamesFactory
    {
        HashSet<string> GetPaletteNames(ICloudVisualization visualization);
    }
}
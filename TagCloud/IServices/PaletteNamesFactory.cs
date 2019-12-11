using System.Collections.Generic;
using System.Linq;

namespace TagCloud.IServices
{
    public class PaletteNamesFactory : IPaletteNamesFactory
    {
        public HashSet<string> GetPaletteNames(ICloudVisualization visualization)
        {
            return visualization.PaletteDictionary
                .Select(p => p.Key)
                .ToHashSet();
        }
    }
}
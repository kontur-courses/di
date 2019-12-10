using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.IServices
{
    public interface IPaletteNamesFactory
    {
        HashSet<string> GetPaletteNames(ICloudVisualization visualization);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public interface IColoredCloud
    {
        IColoredCloud GetFromCloudLayouter(ICloudLayouter cloud, IColoringAlgorithm coloringAlgorithm);
    }
}

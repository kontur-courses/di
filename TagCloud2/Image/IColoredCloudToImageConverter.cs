using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2.Image
{
    public interface IColoredCloudToImageConverter
    {
        System.Drawing.Image GetImage(IColoredCloud cloud);
    }
}

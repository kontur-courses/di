using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2.TextGeometry
{
    public interface IStringToSizeConverter
    {
        public Size Convert(string input, Font font);
    }
}

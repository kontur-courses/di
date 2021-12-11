using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2.TextGeometry
{
    public class StringToRectangleConverter : IStringToSizeConverter
    {
        public Size Convert(string input, Font font)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            var g = Graphics.FromImage(new Bitmap(100, 100));
            var s = g.MeasureString(input, font).ToSize();
            s.Width++;
            s.Height++;
            return s;
        }
    }
}

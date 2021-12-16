using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2.Cloud
{
    public class WhiteColoringAlgorithm : IColoringAlgorithm
    {
        public Color GetColor(Rectangle rectangle)
        {
            return Color.FromArgb(255, 255, 255);
        }
    }
}

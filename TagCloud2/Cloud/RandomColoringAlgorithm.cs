using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public class RandomColoringAlgorithm : IColoringAlgorithm
    {
        public Color GetColor(Rectangle rectangle)
        {
            var r = new Random();
            return Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    class WhiteColorPicker : IColorPicker
    {
        public Brush GenerateColor()
        {
            return Brushes.White;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Palettes
{
    class SimplePalette : IPalette
    {
        public Font Font { get; }
        public Brush Brush { get; }

        public SimplePalette(Font font, Brush brush)
        {
            Font = font;
            Brush = brush;
        }
    }
}

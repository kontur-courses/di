using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class VisualizatorProperties
    {
        public readonly Size ImageSize;
        public readonly FontFamily TextFont;

        public VisualizatorProperties(Size imageSize, FontFamily textFont)
        {
            ImageSize = imageSize;
            TextFont = textFont;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class ImageProperties
    {
        public readonly Size ImageSize;
        public readonly FontFamily TextFont;

        public ImageProperties(Size imageSize, FontFamily textFont)
        {
            ImageSize = imageSize;
            TextFont = textFont;
        }
    }
}

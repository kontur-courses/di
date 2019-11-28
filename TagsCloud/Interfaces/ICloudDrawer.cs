using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud
{
    interface ICloudDrawer
    {
        void Paint(List<Rectangle> rectangles, int borderWidth);
    }
}

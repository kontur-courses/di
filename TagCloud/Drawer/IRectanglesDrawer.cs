using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Drawer
{
    public interface IRectanglesDrawer
    {
        Bitmap CreateImage(IEnumerable<Rectangle> rectangles);
    }
}
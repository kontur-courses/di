using System.Collections.Generic;
using System.Drawing;
using TagCloud.Data;

namespace TagCloud.Drawer
{
    public interface IWordsDrawer
    {
        Bitmap CreateImage(IEnumerable<WordImageInfo> infos, Brush wordsBrush, Brush backgroundBrush);
    }
}
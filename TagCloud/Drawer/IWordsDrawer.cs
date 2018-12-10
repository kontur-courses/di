using System.Collections.Generic;
using System.Drawing;
using TagCloud.Data;

namespace TagCloud.Drawer
{
    public interface IWordsDrawer
    {
        Bitmap CreateImage(IEnumerable<WordImageInfo> infos, Color wordsColor, Color backgroundColor);
    }
}
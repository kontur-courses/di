using System.Drawing;

namespace TagCloud.Drawer;

public interface IDrawer
{
    Bitmap DrawTagCloud(IEnumerable<(string word, int rank)> words);
}
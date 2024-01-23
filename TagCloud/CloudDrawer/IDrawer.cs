using System.Drawing;
using TagCloud.Layouter;

namespace TagCloud.CloudDrawer;

public interface IDrawer
{
    Bitmap DrawTagCloud(IEnumerable<(string word, int rank)> words);
}
using System.Drawing;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization
{
    public interface IWordPainter
    {
        Color GetWordColor(Word word, int index);
    }
}

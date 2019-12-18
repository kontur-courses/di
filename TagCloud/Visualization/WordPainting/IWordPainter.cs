using System.Drawing;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization.WordPainting
{
    public interface IWordPainter
    {
        Color GetWordColor(Word word, int index);
        string Name { get; }
    }
}

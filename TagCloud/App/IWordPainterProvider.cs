using TagCloud.Visualization;
using TagCloud.Visualization.WordPainting;

namespace TagCloud.App
{
    public interface IWordPainterProvider
    {
        IWordPainter GetWordPainter();
    }
}

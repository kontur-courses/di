using System.Drawing;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization.WordPainting
{
    public class RandomColorWordPainter : IWordPainter
    {
        public Color GetWordColor(Word word, int index)
        {
            return ColorUtils.GetRandomColor();
        }

        public string Name => "random";
    }
}

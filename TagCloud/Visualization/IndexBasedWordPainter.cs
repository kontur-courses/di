using System.Drawing;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization
{
    public class IndexBasedWordPainter : IWordPainter
    {
        private readonly Color[] colors;

        public IndexBasedWordPainter(PictureConfig pictureConfig)
        {
            colors = pictureConfig.Palette.WordsColors;
        }

        public Color GetWordColor(Word word, int index)
        {
            return colors[index % colors.Length];
        }
    }
}
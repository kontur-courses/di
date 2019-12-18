using System.Drawing;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization.WordPainting
{
    public class IndexBasedWordPainter : IWordPainter
    {
        private readonly PictureConfig pictureConfig;
        private Color[] Colors => pictureConfig.Palette.WordsColors;

        public IndexBasedWordPainter(PictureConfig pictureConfig)
        {
            this.pictureConfig = pictureConfig;
        }

        public Color GetWordColor(Word word, int index)
        {
            return Colors[index % Colors.Length];
        }

        public string Name => "index";
    }
}
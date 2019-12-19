using System.Drawing;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization.WordPainting
{
    public class IndexBasedWordPainter : IWordPainter
    {
        private readonly PictureConfig pictureConfig;
        public ITagCloudElementsPreparer Preparer { get; set; }
        private Color[] Colors => pictureConfig.Palette.WordsColors;

        public IndexBasedWordPainter(PictureConfig pictureConfig)
        {
            this.pictureConfig = pictureConfig;
        }

        public Color GetWordColor(Word word)
        {
            return Colors[Preparer.CurrentWordIndex % Colors.Length];
        }

        public string Name => "index";
    }
}
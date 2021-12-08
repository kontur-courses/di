using System.Collections.Generic;
using System.Drawing;

//https://github.com/MateoMiller/di/blob/master/HomeExercise.md
namespace CloudTagContainer
{
    public class Visualizer
    {
        private readonly IWordSizer wordSizer;
        private readonly VisualizerSettings settings;
        private readonly ILayouter layouter;

        public Visualizer(
            IWordSizer wordSizer,
            VisualizerSettings settings,
            ILayouter layouter)
        {
            this.wordSizer = wordSizer;
            this.settings = settings;
            this.layouter = layouter;
        }

        public Bitmap Visualize(string[] words)
        {
            var convertedWords = wordSizer.Convert(words, settings.TextFont.Size);
            var image = CreateImage(convertedWords);
            return image;
        }

        private Bitmap CreateImage(List<SizedWord> words)
        {
            var imageSize = settings.ImageSize;
            using var bmp = new Bitmap(imageSize.Width, imageSize.Height);
            using var g = Graphics.FromImage(bmp);
            foreach (var word in words)
            {
                DrawWord(word, g);
            }

            return bmp;
        }

        private void DrawWord(SizedWord word, Graphics g)
        {
            var position = layouter.PutNextRectangle(word.WordSize);
            var brush = new SolidBrush(settings.TextColor);
            g.DrawString(word.Word, settings.TextFont, brush, position);
        }
    }
}
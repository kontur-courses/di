using System.Collections.Generic;
using System.Drawing;

//https://github.com/MateoMiller/di/blob/master/HomeExercise.md
namespace CloudTagContainer
{
    public class Visualizer
    {
        private readonly IFileReader fileReader;
        private readonly IWordsPreprocessor preprocessor;
        private readonly IWordConverter wordConverter;
        private readonly VisualizerSettings settings;
        private readonly ILayouter layouter;

        public Visualizer(IFileReader fileReader,
            IWordsPreprocessor preprocessor,
            IWordConverter wordConverter,
            VisualizerSettings settings,
            ILayouter layouter)
        {
            this.fileReader = fileReader;
            this.preprocessor = preprocessor;
            this.wordConverter = wordConverter;
            this.settings = settings;
            this.layouter = layouter;
        }

        public Bitmap Visualize(string path)
        {
            fileReader.SetPath(path);
            var words = fileReader.ReadWords();
            var preprocessesWords = preprocessor.Preprocess(words);
            var convertedWords = wordConverter.Convert(preprocessesWords);
            var image = CreateImage(convertedWords);
            return image;
        }

        private Bitmap CreateImage(List<IWord> words)
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

        private void DrawWord(IWord word, Graphics g)
        {
            var position = layouter.PutNextRectangle(word.GetSize());
            var brush = new SolidBrush(settings.TextColor);
            g.DrawString(word.GetText(), settings.TextFont, brush, position);
        }
    }
}
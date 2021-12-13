using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class WordCloudCreator : IWordCloudCreator
    {
        private ICloudLayouter cloudLayouter;
        private IWordsContainer wordsContainer;
        private Random rnd;

        public WordCloudCreator(ICloudLayouter cloudLayouter, IWordsContainer wordsContainer)
        {
            this.cloudLayouter = cloudLayouter;
            this.wordsContainer = wordsContainer;
            rnd = new Random();
        }

        public IEnumerable<Word> GetWordCloud(Graphics graphic, ImageSettings imageSettings)
        {
            var words = wordsContainer.GetWords().OrderBy(word => rnd.Next());

            if (!words.Any())
                yield break;

            var imageCenterX = imageSettings.ImageSize.Width / 2;
            var imageCenterY = imageSettings.ImageSize.Height / 2;
            var sizeCoef = (int)(imageSettings.ImageSize.Width / words.Select(word => word.Key.Length).Sum() * Math.PI * 2);

            foreach (var word in words)
            {
                var fontSize = sizeCoef + word.Value;
                var font = new Font(imageSettings.FontFamily, fontSize, imageSettings.FontStyle, GraphicsUnit.Pixel);

                var wordSize = graphic.MeasureString(word.Key, font);

                var rect = cloudLayouter.PutNextRectangle(new Size((int)Math.Ceiling(wordSize.Width), (int)Math.Ceiling(wordSize.Height)));
                rect.X += imageCenterX;
                rect.Y += imageCenterY;

                yield return new Word(rect, word.Key, font);
            }
            cloudLayouter.Rectangles.Clear();

        }
    }
}

using System;
using System.Drawing;
using System.Linq;
using TagsCloudVisualisation.Layouting;
using TagsCloudVisualisation.Text;
using TagsCloudVisualisation.Text.Formatting;

namespace TagsCloudVisualisation.Visualisation
{
    public class WordsCloudDrawer
    {
        private readonly IWordFormatter formatter;
        private readonly ITagCloudLayouter layouter;
        private readonly WordsCloudVisualiser visualiser;

        public WordsCloudDrawer(IWordFormatter formatter, ITagCloudLayouter layouter,
            Func<Point, WordsCloudVisualiser> visualiserFactory)
        {
            this.formatter = formatter;
            this.layouter = layouter;
            visualiser = visualiserFactory.Invoke(this.layouter.CloudCenter);
        }

        public Image DrawWords(WordWithFrequency[] wordsCollection)
        {
            if(wordsCollection.Length == 0)
                throw new ArgumentException($"{nameof(wordsCollection)} is empty", nameof(wordsCollection));

            foreach (var word in wordsCollection.OrderByDescending(x => x.Frequency))
            {
                //TODO apply format to whole collection
                var (wordPosition, formattedWord) =
                    GetWordFormatAndPosition(word.Word, wordsCollection.Length, word.Frequency); 
                visualiser.Draw(wordPosition, formattedWord);
            }

            return visualiser.GetImage()!;
        }

        private (Rectangle wordPosition, FormattedWord formattedWord) GetWordFormatAndPosition(
            string word, int totalWordCount, int index)
        {
            var font = formatter.GetFont(word, totalWordCount, index);
            var wordSize = visualiser.MeasureString(word, font);
            var wordPosition = layouter.PutNextRectangle(Size.Ceiling(wordSize));

            var distanceFromCenter = ComputeDistanceBetween(wordPosition.Location, layouter.CloudCenter);
            var wordColor = formatter.GetBrush(word, distanceFromCenter);

            var formattedWord = new FormattedWord(word, font, wordColor);
            return (wordPosition, formattedWord);
        }

        private static double ComputeDistanceBetween(Point p1, Point p2)
        {
            var xOffset = p1.X - p2.X;
            var yOffset = p1.Y - p2.Y;
            return Math.Sqrt(xOffset * xOffset + yOffset * yOffset);
        }
    }
}
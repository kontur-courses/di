using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TagsCloudVisualisation.Layouting;
using TagsCloudVisualisation.Text;
using TagsCloudVisualisation.Text.Formatting;
using TagsCloudVisualisation.Visualisation;

namespace TagsCloudVisualisation
{
    public sealed class TagCloudGenerator
    {
        private readonly IFontSource fontSource;
        private readonly IColorSource colorSource;
        private readonly ITagCloudLayouter layouter;
        private readonly CloudVisualiser visualiser;

        public TagCloudGenerator(ITagCloudLayouter layouter, Func<Point, CloudVisualiser> visualiser,
            IFontSource fontSource, IColorSource colorSource)
        {
            this.layouter = layouter;
            this.fontSource = fontSource;
            this.colorSource = colorSource;
            this.visualiser = visualiser.Invoke(layouter.CloudCenter);
        }

        public Task<Image> DrawWordsAsync(WordWithFrequency[] wordsCollection, CancellationToken token)
        {
            if (wordsCollection.Length == 0)
                throw new ArgumentException($"{nameof(wordsCollection)} is empty", nameof(wordsCollection));

            return Task.Run(() =>
            {
                foreach (var word in wordsCollection.OrderByDescending(x => x.Frequency))
                {
                    if (token.IsCancellationRequested)
                        break;

                    //TODO apply format to whole collection
                    var (wordPosition, formattedWord) =
                        GetWordFormatAndPosition(word.Word, wordsCollection.Length, word.Frequency);
                    visualiser.Draw(wordPosition, formattedWord);

                    AfterWordDrawn?.Invoke();
                }

                return visualiser.GetImage()!;
            }, token);
        }

        private (Rectangle wordPosition, FormattedWord formattedWord) GetWordFormatAndPosition(
            string word, int totalWordCount, int index)
        {
            var font = fontSource.GetFont(word, totalWordCount, index);
            var wordSize = visualiser.MeasureString(word, font);
            var wordPosition = layouter.PutNextRectangle(Size.Ceiling(wordSize));

            var distanceFromCenter = ComputeDistanceBetween(wordPosition.Location, layouter.CloudCenter);
            var wordColor = colorSource.GetBrush(word, distanceFromCenter);

            var formattedWord = new FormattedWord(word, font, wordColor);
            return (wordPosition, formattedWord);
        }

        private static double ComputeDistanceBetween(Point p1, Point p2)
        {
            var xOffset = p1.X - p2.X;
            var yOffset = p1.Y - p2.Y;
            return Math.Sqrt(xOffset * xOffset + yOffset * yOffset);
        }

        public event Action? AfterWordDrawn;
    }
}
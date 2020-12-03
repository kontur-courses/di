using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TagsCloudVisualisation.Extensions;
using TagsCloudVisualisation.Layouting;
using TagsCloudVisualisation.Text;
using TagsCloudVisualisation.Text.Formatting;
using TagsCloudVisualisation.Visualisation;

namespace TagsCloudVisualisation
{
    public sealed class TagCloudGenerator : IDisposable
    {
        private readonly Graphics stubGraphics = Graphics.FromHwnd(IntPtr.Zero);

        public Task<Image> DrawWordsAsync(IFontSource fontSource,
            IColorSource colorSource,
            ITagCloudLayouter layouter,
            CloudVisualiser visualiser,
            WordWithFrequency[] wordsCollection,
            CancellationToken token)
        {
            if (wordsCollection.Length == 0)
                throw new ArgumentException($"{nameof(wordsCollection)} is empty", nameof(wordsCollection));

            return Task.Run(() =>
            {
                var computedWordsEnumerable = wordsCollection.OrderByDescending(x => x.Frequency)
                    .Select(word => GetWordFormatAndPosition(
                        fontSource, colorSource, layouter,
                        word.Word, wordsCollection.Length, word.Frequency
                    ))
                    .UntilCanceled(token)
                    .OnEach(_ => AfterWordDrawn?.Invoke());

                return visualiser.DrawWords(layouter.CloudCenter, computedWordsEnumerable);
            }, token);
        }

        private (Rectangle wordPosition, FormattedWord formattedWord) GetWordFormatAndPosition(IFontSource fontSource,
            IColorSource colorSource,
            ITagCloudLayouter layouter,
            string word, int totalWordCount, int index)
        {
            var font = fontSource.GetFont(word, totalWordCount, index);
            var wordSize = stubGraphics.MeasureString(word, font);
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

        public void Dispose()
        {
            stubGraphics.Dispose();
        }
    }
}
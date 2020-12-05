using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TagCloud.Core.Layouting;
using TagCloud.Core.Text.Formatting;
using TagCloud.Core.Utils;
using TagCloud.Core.Visualisation;

namespace TagCloud.Core
{
    public sealed class TagCloudGenerator : ITagCloudGenerator
    {
        private readonly Graphics stubGraphics = Graphics.FromHwnd(IntPtr.Zero);

        public async Task<Image> DrawWordsAsync(IFontSizeResolver fontSizeResolver,
            Color[] palette,
            ITagCloudLayouter layouter,
            Dictionary<string, int> wordsCollection,
            CancellationToken token, FontFamily fontFamily)
        {
            if (wordsCollection.Count == 0)
                throw new ArgumentException($"{nameof(wordsCollection)} is empty", nameof(wordsCollection));
            var fontsCollection = fontSizeResolver.GetFontSizesForAll(wordsCollection);

            return await Task.Run(() =>
            {
                var computedWords = wordsCollection
                    .OrderByDescending(x => x.Value)
                    .Select(word => new {Word = word.Key, FontSize = fontsCollection[word.Key]})
                    .Select(x =>
                    {
                        var font = new Font(fontFamily, x.FontSize);
                        var wordSize = Size.Ceiling(stubGraphics.MeasureString(x.Word, font));
                        var location = layouter.PutNextRectangle(wordSize);
                        var brush = new SolidBrush(Randomized.ItemFrom(palette));
                        return (location, new FormattedWord(x.Word, font, brush));
                    });

                using var cloudVisualiser = new CloudVisualiser();
                foreach (var (location, formattedWord) in computedWords)
                {
                    cloudVisualiser.DrawNextWord(location, formattedWord);
                    formattedWord.Dispose();
                    if (token.IsCancellationRequested)
                        break;
                }

                return (Image) cloudVisualiser.Current!.Clone();
            }, token);
        }

        public void Dispose()
        {
            stubGraphics.Dispose();
        }
    }
}
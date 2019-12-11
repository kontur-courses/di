using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudContainer.Api;

namespace TagCloudContainer.Implementations
{
    [CliElement("wordlayouter", typeof(WordCloudLayouter))]
    public class WordCloudLayouter : IWordCloudLayouter
    {
        private readonly ICloudLayouter rectangleLayouter;
        private readonly IStringSizeProvider sizeProvider;

        public WordCloudLayouter(ICloudLayouter rectangleLayouter, IStringSizeProvider sizeProvider)
        {
            this.rectangleLayouter = rectangleLayouter;
            this.sizeProvider = sizeProvider;
        }

        public IReadOnlyDictionary<string, Rectangle> AddWords(
            IReadOnlyDictionary<string, int> words, List<Rectangle> container)
        {
            return words.OrderByDescending(pair => pair.Value)
                .Select(pair => CreateBoundingRectangle(pair.Key, pair.Value, container))
                .ToDictionary(p => p.word, p => p.rect);
        }

        private (string word, Rectangle rect) CreateBoundingRectangle(string word, int occurrenceCount,
            List<Rectangle> container)
        {
            var stringSize = sizeProvider.GetStringSize(word, occurrenceCount) * occurrenceCount;
            var rectangle = rectangleLayouter.PutNextRectangle(stringSize, container);
            return (word, rectangle);
        }
    }
}
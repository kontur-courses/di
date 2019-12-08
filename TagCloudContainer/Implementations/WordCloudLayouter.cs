using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudContainer.Api;

namespace TagCloudContainer.Implementations
{
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
            IReadOnlyDictionary<string, int> words)
        {
            return words.OrderByDescending(pair => pair.Value)
                .Select(pair => CreateBoundingRectangle(pair.Key, pair.Value))
                .ToDictionary(p => p.word, p => p.rect);
        }

        private (string word, Rectangle rect) CreateBoundingRectangle(string word, int occurrenceCount)
        {
            var stringSize = sizeProvider.GetStringSize(word, occurrenceCount);
            var rectangle = rectangleLayouter.PutNextRectangle(stringSize);
            return (word, rectangle);
        }

        public List<Rectangle> Layout => rectangleLayouter.Layout;
    }
}
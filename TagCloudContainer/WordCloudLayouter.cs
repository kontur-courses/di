using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudContainer.Api;

namespace TagCloudContainer
{
    public class WordCloudLayouter : IWordCloudLayouter
    {
        private ICloudLayouter RectangleLayouter;
        private IStringSizeProvider SizeProvider;

        public WordCloudLayouter(ICloudLayouter rectangleLayouter, IStringSizeProvider sizeProvider)
        {
            RectangleLayouter = rectangleLayouter;
            SizeProvider = sizeProvider;
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
            var stringSize = SizeProvider.GetStringSize(word, occurrenceCount);
            var rectangle = RectangleLayouter.PutNextRectangle(stringSize);
            return (word, rectangle);
        }

        public List<Rectangle> Layout => RectangleLayouter.Layout;
    }
}
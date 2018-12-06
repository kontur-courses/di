using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.Layout
{
    public class WordLayout
    {
        private const int MaxWordWidth = 40;
        private const int MinWordWidth = 10;

        private const int MaxWordHeight = 30;
        private const int MinWordHeight = 8;

        private readonly IRectangleLayout layout;
        private readonly Dictionary<string, Rectangle> wordRectangles;

        public ImmutableDictionary<string, Rectangle> WordRectangles => wordRectangles.ToImmutableDictionary();

        public WordLayout(IRectangleLayout layout)
        {
            this.layout = layout;
            wordRectangles = new Dictionary<string, Rectangle>();
        }

        public void PlaceWords(Dictionary<string, int> wordWeights)
        {
            var weightSum = wordWeights.Sum(p => p.Value);
            wordWeights.ToList().Sort((p1, p2) => p1.Value.CompareTo(p2.Value));

            foreach (var pair in wordWeights)
            {
                var coefficient = pair.Value / (double) weightSum;
                var newWidth = MaxWordWidth * coefficient >= MinWordWidth ? MaxWordWidth * coefficient : MinWordWidth;
                var newHeight = MaxWordHeight * coefficient >= MinWordHeight ? MaxWordHeight * coefficient : MinWordHeight;

                var rectangle = layout.PutNextRectangle(new Size((int) newWidth, (int) newHeight));
                wordRectangles.Add(pair.Key, rectangle);
            }
        }
    }
}
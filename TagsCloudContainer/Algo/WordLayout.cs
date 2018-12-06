using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Algo.Geom;
using System.Collections.Immutable;

namespace TagsCloudContainer.Algo
{
    public class WordLayout
    {
        private const int MaxWordWidth = 40;
        private const int MaxWordHeight = 30;

        private readonly CircularCloudLayout layout;
        private readonly Dictionary<string, Rectangle> wordRectangles;

        public ImmutableDictionary<string, Rectangle> WordRectangles => wordRectangles.ToImmutableDictionary();

        public WordLayout(CircularCloudLayout layout)
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
                var wordSize = new Size((int) (MaxWordWidth * coefficient), (int) (MaxWordHeight * coefficient);

                var rectangle = layout.PutNextRectangle(wordSize);
                wordRectangles.Add(pair.Key, rectangle);
            }
        }
    }
}
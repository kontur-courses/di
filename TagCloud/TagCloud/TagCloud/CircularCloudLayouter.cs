using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class CircularCloudLayouter : TagCloud
    {
        private readonly ICurve curve;

        public CircularCloudLayouter(
            ICurve curve,
            IWordsProvider wordsProvider,
            IWordsFilter wordsFilter)
        {
            this.curve = curve;
            WordsProvider = wordsProvider;
            Filter = wordsFilter;
            WordsFilter = wordsFilter;
        }

        public override IWordsProvider WordsProvider { get; }
        public override IWordsFilter WordsFilter { get; }
        public IWordsFilter Filter { get; }

        public override WordRectangle PutNextWord(string word, Size rectangleSize)
        {
            while (true)
            {
                var currentPoint = curve.CurrentPoint;
                var possibleRectangle = new Rectangle(currentPoint, rectangleSize);
                var canFit = WordRectangles.All(rect => !rect.Rectangle.IntersectsWith(possibleRectangle));
                curve.Next();
                if (!canFit) continue;
                var wordRectangle = new WordRectangle(possibleRectangle, word);
                WordRectangles.Add(wordRectangle);
                return wordRectangle;
            }
        }
    }
}
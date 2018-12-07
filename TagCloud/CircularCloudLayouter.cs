using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud;

namespace TagsCloudVisualization 
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private const int MinWordCount = 10;
        private const int MaxWordCount = 80;
        private const double HeightToWidthIndex = 0.75;
        public readonly Point Center;
        private readonly IEnumerator<Point> pointMaker;
        private readonly Dictionary<Rectangle, string> rectangles;
        public IReadOnlyDictionary<Rectangle, string> Rectangles => rectangles;

        private void AddRectangle(Rectangle newRectangle, string word)
        {
            rectangles.Add(newRectangle, word);
        }

        public CircularCloudLayouter(Point center, double spiralStep)
        {
            this.Center = center;
            pointMaker = ArchimedesSpiralPointsMaker
                .GenerateNextPoint(center, spiralStep)
                .GetEnumerator();
            rectangles = new Dictionary<Rectangle, string>();
        }

        private bool AreRectanglesIntersectWith(Rectangle newRectangle)
        {
            return Rectangles.Any((rectangle) => rectangle.Key.IntersectsWith(newRectangle));
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException();
            while (true)
            {
                pointMaker.MoveNext();
                var location = pointMaker.Current;
                var newRectangle = new Rectangle(location, rectangleSize);
                if (!AreRectanglesIntersectWith(newRectangle))
                {
                    return newRectangle;
                }
            }
        }

        public void AddWordsFromDictionary(Dictionary<string, int> words)
        {
            foreach (var word in words)
            {
                if (word.Value < MinWordCount 
                    || word.Value > MaxWordCount) continue;
                var width = (int)(word.Key.Length * word.Value * HeightToWidthIndex);
                var height = word.Value;
                var rectSize = new Size(width, height);
                var newRectangle = PutNextRectangle(rectSize);
                AddRectangle(newRectangle, word.Key);
            }
        }
    }
}

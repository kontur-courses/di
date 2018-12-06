using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        public readonly Point Center;
        private readonly IEnumerator<Point> pointMaker;
        private readonly Dictionary<Rectangle, string> rectangles;
        public IReadOnlyDictionary<Rectangle, string> Rectangles
        {
            get { return rectangles; }
        }

        private void AddRectangle(Rectangle newRectangle, string word)
        {
            rectangles.Add(newRectangle, word);
        }

        public CircularCloudLayouter(Point center, double spiraleStep)
        {
            this.Center = center;
            pointMaker = ArchimedesSpiralePointsMaker
                .GenerateNextPoint(center, spiraleStep)
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
                if (word.Value < 10 || word.Value > 80) continue;
                var width = (int)(word.Key.Length * word.Value * 0.75);
                var height = word.Value;
                var rectSize = new Size(width, height);
                var newRectagle = PutNextRectangle(rectSize);
                AddRectangle(newRectagle, word.Key);
            }
        }
    }
}

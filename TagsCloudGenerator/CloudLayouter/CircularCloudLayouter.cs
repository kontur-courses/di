using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudGenerator.Tools;

namespace TagsCloudGenerator.CloudLayouter
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly Cloud cloud;
        private readonly IEnumerator<Point> layoutPointsEnumerator;

        public CircularCloudLayouter(Point center, ILayoutPointsGenerator layoutPointsGenerator)
        {
            this.layoutPointsEnumerator = layoutPointsGenerator.GetPoints().GetEnumerator();
            cloud = new Cloud(center);
        }

        public Cloud LayoutWords(Dictionary<string, int> wordToCount, Font font)
        {
            var sortedWordsToCount = wordToCount.OrderByDescending(x => x.Value);

            foreach (var item in sortedWordsToCount)
            {
                var word = item.Key;
                var count = item.Value;
                var size = GetSizeOfWord(word, count, font);
                PutNextWord(word, count, size);
            }

            return cloud;
        }

        private static Size GetSizeOfWord(string word, int count, Font font)
        {
            return TextRenderer.MeasureText(word,
                new Font(font.FontFamily, font.Size * count));
        }

        private Word PutNextWord(string value, int count, Size rectangleSize)
        {
            var position = GetRectangleLocation(rectangleSize);
            var rectangle = new Rectangle(position, rectangleSize);
            var word = new Word(value, rectangle, count);

            cloud.Words.Add(word);

            return word;
        }

        private Point GetRectangleLocation(Size rectangleSize)
        {
            (bool success, Point location) nextLocation;
            do
            {
                nextLocation = TryGetNextLocation(rectangleSize);
            } while (!nextLocation.success);

            var location = nextLocation.location;

            if (cloud.Words.Count == 0)
                return location;

            location = TryMove(rectangleSize, location, cloud.Center);
            var previous = cloud.Words.Last().Rectangle.Location;

            return TryMove(rectangleSize, location, previous);
        }

        private (bool success, Point location) TryGetNextLocation(Size rectangleSize)
        {
            layoutPointsEnumerator.MoveNext();
            var center = layoutPointsEnumerator.Current;

            var upperLeftCorner = GetUpperLeftCornerPosition(rectangleSize, center);
            var rectangle = new Rectangle(upperLeftCorner, rectangleSize);

            return (NotIntersectsWithOther(rectangle), upperLeftCorner);
        }


        private Point TryMove(Size rectangleSize, Point from, Point to)
        {
            var newLocation = from;
            var nearestToTarget = to;
            var minDistance = Math.Sqrt(2);

            while (newLocation.Distance(nearestToTarget) > minDistance)
            {
                var middle = newLocation.GetMiddlePoint(nearestToTarget);
                var rectangle = new Rectangle(middle, rectangleSize);

                if (NotIntersectsWithOther(rectangle))
                    newLocation = middle;
                else
                    nearestToTarget = middle;
            }

            return newLocation;
        }

        private bool NotIntersectsWithOther(Rectangle rectangle)
        {
            return cloud.Words.All(r => !r.Rectangle.IntersectsWith(rectangle));
        }

        protected Point GetUpperLeftCornerPosition(Size rectangleSize, Point center)
        {
            var xOffset = rectangleSize.Width / 2;
            var yOffset = rectangleSize.Height / 2;

            return new Point(center.X - xOffset, center.Y - yOffset);
        }
    }
}
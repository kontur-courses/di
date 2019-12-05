using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagsCloudGenerator.Tools;

namespace TagsCloudGenerator.CloudLayouter
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly Cloud cloud;
        private readonly IEnumerator<Point> spiralEnumerator;
        private readonly Size unitRectangle;

        public CircularCloudLayouter(Point center, Size unitRectangleSize)
        {
            unitRectangle = unitRectangleSize;
            cloud = new Cloud(center);
            spiralEnumerator = SpiralGenerator.GetSpiral(center, 0.5, Math.PI / 16).GetEnumerator();
        }

        public Cloud LayoutWords(Dictionary<string, int> wordToCount, Font font)
        {
            foreach (var item in wordToCount)
            {
                var word = item.Key;
                var count = item.Value;
                var size = GetSizeOfWord(word, count, font);
                PutNextWord(word, count, size);
            }

            return cloud;
        }

        private Size GetSizeOfWord(string word, int count, Font font)
        {
            return TextRenderer.MeasureText(word,
                new Font(font.FontFamily, font.Size * count));
        }

        private void PutNextWord(string value, int count, Size rectangleSize)
        {
            var position = GetRectangleLocation(rectangleSize);
            var rectangle = new Rectangle(position, rectangleSize);
            var word = new Word(value, rectangle, count);

            cloud.Words.Add(word);
        }

        private Point GetRectangleLocation(Size rectangleSize)
        {
            Point location;

            while (!TryGetNextLocation(rectangleSize, out location))
            {
                //Should be empty
            }

            if (cloud.Words.Count == 0)
                return location;

            location = TryMove(rectangleSize, location, cloud.Center);
            var previous = cloud.Words.Last().Rectangle.Location;

            return TryMove(rectangleSize, location, previous);
        }

        private bool TryGetNextLocation(Size rectangleSize, out Point upperLeftCorner)
        {
            spiralEnumerator.MoveNext();
            var center = spiralEnumerator.Current;

            upperLeftCorner = GetUpperLeftCornerPosition(rectangleSize, center);
            var rectangle = new Rectangle(upperLeftCorner, rectangleSize);

            return NotIntersectsWithOther(rectangle);
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
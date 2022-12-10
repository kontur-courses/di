using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Infrastructure;
using TagsCloudVisualization;

namespace TagsCloudContainer.Algorithm
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        

        private readonly ImageSettings imageSettings;
        private readonly List<(Rectangle rectangle, string text)> rectangles;
        private readonly IParser parser;
        private Func<int, Point> pointFinderFunc;

        public Point Center => new Point(imageSettings.Width / 2, imageSettings.Height / 2);

        public CircularCloudLayouter(ImageSettings imageSettings, AlgorithmSettings algoSettings, IParser parser)
        {
            algoSettings.ThrowExcIfNonPositiveArgs();
            imageSettings.ThrowExcIfNonPositiveArgs();

            this.imageSettings = imageSettings;
            this.pointFinderFunc = algoSettings.GetPointFinderFunction(Center);
            rectangles = new List<(Rectangle rectangle, string text)>();
            this.parser = parser;
        }

        public List<(Rectangle rectangle, string text)> FindRectanglesPositions()
        {
            rectangles.Clear();
            var wordsCount = parser.GetWordsCountWithoutBoring();
            var sumWords = wordsCount.Sum(e => e.Value);
            foreach (var pair in wordsCount)
            {
                var size = CalculateRectangleSize(pair.Value, sumWords);
                rectangles.Add((GetNextRectangle(size), pair.Key));
            }

            return rectangles;
        }

        private Size CalculateRectangleSize(int wordCount, int totalWordsCount)
        {
            var proc = (double)wordCount / (double)totalWordsCount;
            return new Size((int)(imageSettings.Width * 0.7 * proc),
                (int)(imageSettings.Height * 0.7 * proc));
        }

        private Rectangle GetNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("only positive size");
            var rect = FindFreePlaceForNewRectangle(rectangleSize);
            TryMoveRectangleCloserToCenter(ref rect);
            return rect;
        }

        private void TryMoveRectangleCloserToCenter(ref Rectangle rect)
        {
            var xStep = rect.GetCenter().X > Center.X ? new Point(-1, 0) : new Point(1, 0);
            TryMoveRectangleToTarget(rect.GetCenter().X, Center.X, xStep, ref rect);

            var yStep = rect.GetCenter().Y > Center.Y ? new Point(0, -1) : new Point(0, 1);
            TryMoveRectangleToTarget(rect.GetCenter().Y, Center.Y, yStep, ref rect);
        }

        private void TryMoveRectangleToTarget(int startPos, int targetPos, Point stepPoint, ref Rectangle rect)
        {
            var step = targetPos > startPos ? 1 : -1;
            while (NotIntersectOthers(rect) && startPos != targetPos)
            {
                startPos += step;
                rect.Location = rect.Location.Plus(stepPoint);
            }

            if (!NotIntersectOthers(rect))
                rect.Location = rect.Location.Minus(stepPoint);
        }

        private Rectangle FindFreePlaceForNewRectangle(Size rectangleSize)
        {
            var arg = 0;
            while (true)
            {
                var rectCenter = pointFinderFunc(arg);
                var x = rectCenter.X - rectangleSize.Width / 2;
                var y = rectCenter.Y - rectangleSize.Height / 2;
                var rect = new Rectangle(new Point(x, y), rectangleSize);
                if (NotIntersectOthers(rect))
                    return rect;
                arg++;
            }
        }

        private bool NotIntersectOthers(Rectangle rect)
        {
            return rectangles.All(pair => !rect.IntersectsWith(pair.rectangle));
        }
    }
}

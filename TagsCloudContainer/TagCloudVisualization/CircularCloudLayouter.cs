using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.WordProcessing;

namespace TagsCloudContainer.TagCloudVisualization
{
    public class CircularCloudLayouter : ILayouter
    {
        public readonly Point Center;
        private readonly List<Rectangle> rectangles;
        public IEnumerable<Rectangle> Rectangles => new List<Rectangle>(rectangles);
        private readonly IPlacingFunc placingFunc;
        private double placingFuncParam;
        private readonly int baseWidth;
        private readonly int baseHeight;

        public CircularCloudLayouter(IPlacingFunc placingFunc, Font font)
        {
            this.placingFunc = placingFunc;
            baseWidth = Convert.ToInt32(font.Size);
            baseHeight = font.Height;
            Center = new Point(0, 0);
            rectangles = new List<Rectangle>();
        }

        public CircularCloudLayouter(IPlacingFunc placingFunc)
            : this(placingFunc, new Font("Arial", 1))
        {
        }

        public IEnumerable<TagCloudItem> PlaceWords(IEnumerable<WordData> words)
        {
            var wordDatas = words.ToList();
            var minCount = wordDatas.Select(w => w.Count).Min();
            var maxCount = wordDatas.Select(w => w.Count).Max();

            wordDatas.Sort((a, b) => a.Count.CompareTo(b.Count));
            wordDatas.Reverse();
            var result = new List<TagCloudItem>();
            foreach (var word in wordDatas)
            {
                var coefficient = word.Count == minCount
                    ? 1
                    : (word.Count - minCount) / (maxCount - minCount) + 1;
                var width = word.Word.Length * baseWidth * coefficient;
                var height = baseHeight * coefficient;
                result.Add(PlaceNextWord(word, new Size(width, height), coefficient));
            }

            return result;
        }

        private TagCloudItem PlaceNextWord(WordData word, Size size, int coefficient)
        {
            return new TagCloudItem(PutNextRectangle(size), word.Word, coefficient);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle rectangle;
            if (rectangles.Count == 0)
            {
                var location = new Point(Center.X - rectangleSize.Width / 2,
                    Center.Y - rectangleSize.Height / 2);
                rectangle = new Rectangle(location, rectangleSize);
            }
            else
            {
                rectangle = new Rectangle(new Point(0, 0), rectangleSize);
                PlaceOnSpiral(ref rectangle);
                SealInLayout(ref rectangle);
            }

            rectangles.Add(rectangle);

            return rectangle;
        }

        private void PlaceOnSpiral(ref Rectangle rectangle)
        {
            while (RectangleIntersectsWithLayout(rectangle))
            {
                var newRectCenter = placingFunc.CalculatePoint(placingFuncParam);
                rectangle.Location = new Point(newRectCenter.X - rectangle.Size.Width / 2,
                    newRectCenter.Y - rectangle.Size.Height / 2);
                placingFuncParam += 0.1;
            }
        }

        private void SealInLayout(ref Rectangle rectangle)
        {
            var rectCenter = rectangle.GetCenter();
            var dx = Math.Sign(Center.X - rectCenter.X);
            var dy = Math.Sign(Center.Y - rectCenter.Y);
            var done = false;
            while (!done)
            {
                rectangle.X += dx;
                rectangle.Y += dy;

                if (!RectangleIntersectsWithLayout(rectangle))
                    continue;

                rectangle.X -= dx;
                rectangle.Y -= dy;
                done = true;
            }
        }

        private bool RectangleIntersectsWithLayout(Rectangle rectangle)
        {
            return rectangles.Select(rectangle.IntersectsWith).Any(r => r);
        }
    }

    public static class RectangleExtensions
    {
        public static Point GetCenter(this Rectangle rect)
        {
            return new Point(rect.Left + rect.Width / 2,
                rect.Top + rect.Height / 2);
        }
    }
}
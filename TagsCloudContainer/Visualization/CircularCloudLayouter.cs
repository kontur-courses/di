using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.RectangleTranslation;
using TagsCloudContainer.Visualization.Interfaces;

namespace TagsCloudContainer.Visualization
{
    public class CircularCloudLayouter : IWordLayouter
    {
        private List<WordRectangle> wordRectangles = new List<WordRectangle>();
        private readonly Point center = new Point(0, 0);
        private readonly ILayoutAlgorithm layoutAlgorithm;

        public CircularCloudLayouter()
        {
            layoutAlgorithm = new ArchimedeanSpiral(center);
        }

        public List<WordRectangle> LayoutWords(IEnumerable<SizedWord> sizedWords)
        {
            var layout = sizedWords.Select(PutNextWord).ToList();
            var rectanglesWithOffset = OffsetCenter(layout);
            wordRectangles = rectanglesWithOffset;
            return rectanglesWithOffset;
        }

        public List<WordRectangle> GetWordsRectangles()
        {
            return wordRectangles
                .Select(rectangle => new WordRectangle(
                    new SizedWord(rectangle.SizedWord.Word, rectangle.SizedWord.FontSize),
                    new RectangleF(rectangle.Rectangle.X,
                        rectangle.Rectangle.Y,
                        rectangle.Rectangle.Width,
                        rectangle.Rectangle.Height)))
                .ToList();
        }

        private List<WordRectangle> OffsetCenter(List<WordRectangle> rectangles)
        {
            var minX = rectangles.Min(wordRectangle => wordRectangle.Rectangle.Left);
            var xOffset = minX < 0 ? -minX + 10 : 0;
            var minY = rectangles.Min(wordRectangle => wordRectangle.Rectangle.Top);
            var yOffset = minY < 0 ? -minY + 10 : 0;
            var rectanglesWithOffset = rectangles.Select(wordRectangle => new WordRectangle(wordRectangle.SizedWord,
                new RectangleF(wordRectangle.Rectangle.X + xOffset, wordRectangle.Rectangle.Y + yOffset,
                    wordRectangle.Rectangle.Width, wordRectangle.Rectangle.Height))).ToList();
            return rectanglesWithOffset;
        }

        private WordRectangle PutNextWord(SizedWord sizedWord)
        {
            var rectangleSize = sizedWord.WordSize;
            var point = layoutAlgorithm.GetNextPoint();
            var checkedRectangle = new RectangleF(point, rectangleSize);
            while (!IsCorrectToPlace(checkedRectangle))
            {
                point = layoutAlgorithm.GetNextPoint();
                checkedRectangle = new RectangleF(point, rectangleSize);
            }

            var adjustedRectangle = AdjustRectangle(checkedRectangle);
            var wordRectangle = new WordRectangle(sizedWord, adjustedRectangle);
            wordRectangles.Add(wordRectangle);
            return wordRectangle;
        }

        private RectangleF AdjustRectangle(RectangleF rectangle)
        {
            rectangle = MoveRectangleHorizontally(rectangle);
            rectangle = MoveRectangleVertically(rectangle);
            return rectangle;
        }

        private RectangleF MoveRectangleHorizontally(RectangleF rectangle)
        {
            var stepSize = rectangle.X < center.X ? 1 : -1;
            var checkedRectangle = new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            while (IsCorrectToPlace(checkedRectangle) && Math.Abs(checkedRectangle.X - center.X) > 0.5)
            {
                checkedRectangle.X += stepSize;
            }

            if (!IsCorrectToPlace(checkedRectangle))
            {
                checkedRectangle.X -= stepSize;
            }

            return checkedRectangle;
        }

        private RectangleF MoveRectangleVertically(RectangleF rectangle)
        {
            var stepSize = rectangle.Y < center.Y ? 1 : -1;
            var checkedRectangle = new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            while (IsCorrectToPlace(checkedRectangle) && Math.Abs(checkedRectangle.Y - center.Y) > 0.5)
            {
                checkedRectangle.Y += stepSize;
            }

            if (!IsCorrectToPlace(checkedRectangle))
            {
                checkedRectangle.Y -= stepSize;
            }

            return checkedRectangle;
        }

        private bool IsCorrectToPlace(RectangleF checkedRectangle)
        {
            return wordRectangles.All(rectangle => !rectangle.Rectangle.IntersectsWith(checkedRectangle));
        }
    }
}
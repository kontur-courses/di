using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.LayouterAlgorithms
{
    /// <summary>
    /// Возвращает последовательность непересекающихся прямоугольников основанных на точках спирали. 
    /// </summary>
    public class CircularCloudLayouter : ICloudLayouterAlgorithm
    {
        public List<Rectangle> Rectangles { get; }
        private Spiral Spiral { get; }


        public CircularCloudLayouter(Spiral spiral)
        {
            Rectangles = new List<Rectangle>();
            Spiral = spiral;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height < 0 || rectangleSize.Width < 0)
                throw new ArgumentException("Wrong size of rectangle");
            var rect = Spiral.GetPoints()
                .Select(point =>
                {
                    var coordinatesOfRectangle =
                        RectangleCoordinatesCalculator.CalculateRectangleCoordinates(point, rectangleSize);
                    return new Rectangle(coordinatesOfRectangle, rectangleSize);
                })
                .First(rectangle => !IntersectsWithOtherRectangles(rectangle));
            Rectangles.Add(rect);
            return rect;
        }

        public Point PlaceNextWord(string word, int wordCount, int coefficient)
        {
            var rectangleHeight = wordCount * coefficient * word.Length + coefficient;
            var rectangleWidth = wordCount * 2 * coefficient;
            var rectangle = PutNextRectangle(new Size(rectangleHeight, rectangleWidth));
            return new Point(rectangle.X, rectangle.Y);
        }

        private bool IntersectsWithOtherRectangles(Rectangle rectangle)
        {
            return Rectangles.Any(r => r.IntersectsWith(rectangle));
        }
    }
}
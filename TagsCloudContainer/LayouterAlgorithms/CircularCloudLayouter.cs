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
        private Dictionary<string, int> Words { get; }

        public int Coefficient { get; }

        public CircularCloudLayouter(Spiral spiral, InputFileHandler handler, CoefficientCalculator calculator)
        {
            Rectangles = new List<Rectangle>();
            Spiral = spiral;
            Words = handler.FormFrequencyDictionary();
            Coefficient = calculator.CalculateCoefficient();
        }

        public Dictionary<Tuple<string, int>, Rectangle> GetWordRectangleDictionary()
        {
            var result = new Dictionary<Tuple<string, int>, Rectangle>();
            foreach (var pair in Words)
            {
                var word = pair.Key;
                var countOfWord = pair.Value;
                var rectangleHeight = countOfWord * Coefficient * word.Length + Coefficient;
                var rectangleWidth = countOfWord * 2 * Coefficient;
                result.Add(new Tuple<string, int>(word, countOfWord),
                    PutNextRectangle(new Size(rectangleHeight, rectangleWidth)));
            }

            return result;
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

        private bool IntersectsWithOtherRectangles(Rectangle rectangle)
        {
            return Rectangles.Any(r => r.IntersectsWith(rectangle));
        }
    }
}
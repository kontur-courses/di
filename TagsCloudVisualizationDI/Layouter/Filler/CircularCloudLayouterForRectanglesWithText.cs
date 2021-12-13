using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualizationDI.Layouter.Filler
{
    public class CircularCloudLayouterForRectanglesWithText : IContentFiller
    {
        private Point Center { get; }

        private Spiral LayouterSpiral { get; }


        public CircularCloudLayouterForRectanglesWithText(Point center)
        {
            Center = center;
            LayouterSpiral = new Spiral();
        }

        private RectangleWithWord CreateNewRectangleWithWord(Size rectangleSize, Word inputWord)
        {
            var rectangleCenterLocation = LayouterSpiral.GetNextPoint(Center);
            var rectangleX = rectangleCenterLocation.X - Math.Abs(rectangleSize.Width / 2);
            var rectangleY = rectangleCenterLocation.Y - Math.Abs(rectangleSize.Height / 2);
            var rectangle = new Rectangle(rectangleX, rectangleY, Math.Abs(rectangleSize.Width), Math.Abs(rectangleSize.Height));
            return new RectangleWithWord(rectangle, inputWord);
        }

        public Dictionary<string, RectangleWithWord> FormStatisticElements(Size elementSize, List<Word> startElements)
        {
            var dictWithFormedWords = new Dictionary<string, RectangleWithWord>();

            foreach (var word in startElements)
            {
                var element = CreateNewRectangleWithWord(elementSize, word);

                if (dictWithFormedWords.ContainsKey(word.WordText))
                    dictWithFormedWords[word.WordText].WordElement.CntOfWords++;
                else
                    dictWithFormedWords[word.WordText] = element;
            }
            return dictWithFormedWords;
        }

        public List<RectangleWithWord> MakePositionElements(List<RectangleWithWord> sizedElements)
        {
            var positionedElements = new List<RectangleWithWord>();
            foreach (var element in sizedElements)
            {
                var elementSize = element.RectangleElement.Size;
                var word = element.WordElement;
                var nextRectangle = CreateNewRectangleWithWord(elementSize, word);

                while (positionedElements.Any(rectangle => rectangle
                    .RectangleElement.IntersectsWith(nextRectangle.RectangleElement)))
                {
                    nextRectangle = CreateNewRectangleWithWord(nextRectangle.RectangleElement.Size, word);
                }

                positionedElements.Add(nextRectangle);
            }
            return positionedElements;
        }
    }
}

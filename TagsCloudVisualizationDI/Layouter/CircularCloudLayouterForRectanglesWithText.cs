using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualizationDI.Layouter
{
    public class CircularCloudLayouterForRectanglesWithText : ICircularCloudLayouter, IContentFiller
    {
        private Point Center { get; }

        private Spiral LayouterSpiral { get; }

        private Dictionary<string, RectangleWithWord> RectangleDict { get; }



        public CircularCloudLayouterForRectanglesWithText(Point center)
        {
            Center = center;
            LayouterSpiral = new Spiral();
            RectangleDict = new Dictionary<string, RectangleWithWord>();
        }

        public RectangleWithWord PutNextElement(RectangleWithWord rectangleWithWord)
        {
            var rectangleSize = rectangleWithWord.RectangleElement.Size;
            var word = rectangleWithWord.WordElement;

            if (rectangleSize.Width == 0 || rectangleSize.Height == 0)
                throw new ArgumentException();

            if (RectangleDict.ContainsKey(word.WordText))
            {
                RectangleDict[word.WordText].WordElement.CntOfWords++;
                return RectangleDict[word.WordText];
            }

            var nextRectangle = CreateNewRectangleWithWord(rectangleSize, word);
            while (RectangleDict.Values.Any(rectangle => rectangle.RectangleElement.IntersectsWith(nextRectangle.RectangleElement)))
                nextRectangle = CreateNewRectangleWithWord(rectangleSize, word);
            if (nextRectangle.RectangleElement.Location != Center)
                nextRectangle = CenterElement(nextRectangle);

            RectangleDict.Add(word.WordText, nextRectangle);

            return nextRectangle;
        }

        public List<RectangleWithWord> GetElementsList() => RectangleDict.Values.ToList();

        private RectangleWithWord CreateNewRectangleWithWord(Size rectangleSize, Word word)
        {
            var rectangleCenterLocation = LayouterSpiral.GetNextPoint(Center);
            var rectangleX = rectangleCenterLocation.X - Math.Abs(rectangleSize.Width / 2);
            var rectangleY = rectangleCenterLocation.Y - Math.Abs(rectangleSize.Height / 2);
            var rectangle = new Rectangle(rectangleX, rectangleY, Math.Abs(rectangleSize.Width), Math.Abs(rectangleSize.Height));
            return new RectangleWithWord(rectangle, word);
        }

        private RectangleWithWord CenterElement(RectangleWithWord inputRectangleWithWord)
        {
            var centeringRectangleElement = ElementCentering.Centering(inputRectangleWithWord.RectangleElement, Center, RectangleDict);
            inputRectangleWithWord.RectangleElement = centeringRectangleElement;
            //var rectangleElement = inputRectangleWithWord.RectangleElement;
            /*
            var directionXSign = Math.Sign(Center.X - rectangleElement.X);
            var directionYSign = Math.Sign(Center.Y - rectangleElement.Y);

            
            
            while (!IsIntersect(rectangleElement))
            {
                if (rectangleElement.Y == Center.Y)
                    break;
                rectangleElement.Offset(0, directionYSign);
            }
            rectangleElement.Offset(0, -directionYSign);

            while (!IsIntersect(rectangleElement))
            {
                if (rectangleElement.X == Center.X)
                    break;
                rectangleElement.Offset(directionXSign, 0);
            }
            rectangleElement.Offset(-directionXSign, 0);
            if (RectangleDict.Count == 0)
                rectangleElement.Offset(directionXSign, directionYSign);
            
            if (IsIntersect(rectangleElement))
                rectangleElement.Offset(-directionXSign, -directionYSign);
            */

            //inputRectangleWithWord.RectangleElement = rectangleElement;


            return inputRectangleWithWord;
        }

        public void FillInElements(Size elementSize, List<Word> wordList)
        {

            foreach (var word in wordList)
            {
                var element = CreateNewRectangleWithWord(elementSize, word);
                PutNextElement(element);
            }
        }

        /*
        private bool IsIntersect(Rectangle inputRectangle) =>
            RectangleDict.Select(el => el.Value)
                .Any(rect => rect.RectangleElement.IntersectsWith(inputRectangle));
        */
    }
}

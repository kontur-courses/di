using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Layouter
{
    public class CircularCloudLayouterForRectanglesWithText : ICircularCloudLayouter
    {
        private Point Center { get; }

        private Spiral LayouterSpiral { get; }

        //private HashSet<RectangleWithWord> RectangleList { get; }

        private Dictionary<Word, RectangleWithWord> RectangleList { get; set; }



        public CircularCloudLayouterForRectanglesWithText(Point center)
        {
            Center = center;
            LayouterSpiral = new Spiral();
            RectangleList = new Dictionary<Word, RectangleWithWord>();
        }

        public RectangleWithWord PutNextElement(Size rectangleSize, Word word)
        {
            if (rectangleSize.Width == 0 || rectangleSize.Height == 0)
                throw new ArgumentException();

            if (RectangleList.ContainsKey(word))
            {
                RectangleList[word].WordElement.CntOfWords++;
                return RectangleList[word];
            }

            var nextRectangle = CreateNewRectangleWithWord(rectangleSize, word);
            while (RectangleList.Values.Any(rectangle => rectangle.RectangleElement.IntersectsWith(nextRectangle.RectangleElement)))
                nextRectangle = CreateNewRectangleWithWord(rectangleSize, word);
            if (nextRectangle.RectangleElement.Location != Center)
                nextRectangle = CenterElement(nextRectangle);

            RectangleList.Add(word, nextRectangle);



            return nextRectangle;
        }

        public List<RectangleWithWord> GetElementsList()
        {
            return RectangleList.Values.ToList();
        }

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

            var directionXSign = Math.Sign(Center.X - inputRectangleWithWord.RectangleElement.X);
            var directionYSign = Math.Sign(Center.Y - inputRectangleWithWord.RectangleElement.Y);

            var rectangleElement = inputRectangleWithWord.RectangleElement;

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
            
            if (RectangleList.Count == 0)
                rectangleElement.Offset(directionXSign, directionYSign);

            inputRectangleWithWord.RectangleElement = rectangleElement;

            if (IsIntersect(inputRectangleWithWord.RectangleElement))
                inputRectangleWithWord.RectangleElement.Offset(-directionXSign, -directionYSign);

            return inputRectangleWithWord;
        }

        public void FillInElements(Size elementSize, List<Word> wordList)
        {
            foreach (var word in wordList)
            {
                PutNextElement(elementSize, word);
            }
        }

        private bool IsIntersect(Rectangle inputRectangle) =>
            RectangleList.Select(el => el.Value)
                .Any(rect => rect.RectangleElement.IntersectsWith(inputRectangle));
    }
}

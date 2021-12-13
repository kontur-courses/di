using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualizationDI.TextAnalization.Visualization;

namespace TagsCloudVisualizationDI.Layouter.Filler
{
    public class CircularCloudLayouterForRectanglesWithText : IContentFiller
    {
        private Point Center { get; }

        private Spiral LayouterSpiral { get; }

        private Dictionary<string, RectangleWithWord> ElementsDict { get;}



        public CircularCloudLayouterForRectanglesWithText(Point center)
        {
            Center = center;
            LayouterSpiral = new Spiral();
            ElementsDict = new Dictionary<string, RectangleWithWord>();
        }

        
        private void PutNextElement(RectangleWithWord rectangleWithWord)
        {
            var rectangleSize = rectangleWithWord.RectangleElement.Size;
            var word = rectangleWithWord.WordElement;

            if (rectangleSize.Width == 0 || rectangleSize.Height == 0)
                throw new ArgumentException("Width and height can't be zero");

            if (ElementsDict.ContainsKey(word.WordText))
            {
                ElementsDict[word.WordText].WordElement.CntOfWords++;
                return;
            }

            var nextRectangle = CreateNewRectangleWithWord(rectangleSize, word);

            while (ElementsDict.Values.Any(rectangle =>
                rectangle.RectangleElement.IntersectsWith(nextRectangle.RectangleElement)))
                nextRectangle = CreateNewRectangleWithWord(rectangleSize, word);
            if(nextRectangle.RectangleElement.Location != Center)
                nextRectangle = CenterElement(nextRectangle);

            ElementsDict.Add(word.WordText, nextRectangle);


        }

        public List<RectangleWithWord> GetElementsList() => ElementsDict.Values.ToList();

        private RectangleWithWord CreateNewRectangleWithWord(Size rectangleSize, Word inputWord)
        {
            var rectangleCenterLocation = LayouterSpiral.GetNextPoint(Center);
            var rectangleX = rectangleCenterLocation.X - Math.Abs(rectangleSize.Width / 2);
            var rectangleY = rectangleCenterLocation.Y - Math.Abs(rectangleSize.Height / 2);
            var rectangle = new Rectangle(rectangleX, rectangleY, Math.Abs(rectangleSize.Width), Math.Abs(rectangleSize.Height));
            return new RectangleWithWord(rectangle, inputWord);
        }

        

        public void FillInElements(Size elementSize, List<Word> wordList)
        {
            foreach (var word in wordList)
            {
                var element = CreateNewRectangleWithWord(elementSize, word);
                PutNextElement(element);
            }
        }

        private RectangleWithWord CenterElement(RectangleWithWord inputRectangleWithWord)
        {
            var centeringRectangleElement = ElementCentering.Centering(inputRectangleWithWord.RectangleElement, Center, ElementsDict);
            inputRectangleWithWord.RectangleElement = centeringRectangleElement;
            return inputRectangleWithWord;
        }
    }
}

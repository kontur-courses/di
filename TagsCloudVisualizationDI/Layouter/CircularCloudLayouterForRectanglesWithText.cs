using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualizationDI.Visualization;

namespace TagsCloudVisualizationDI.Layouter
{
    public class CircularCloudLayouterForRectanglesWithText : IContentFiller
    {
        private Point Center { get; }

        private Spiral LayouterSpiral { get; }

        private Func<List<RectangleWithWord>, IVisualization> Visualization { get; }

        private Dictionary<string, RectangleWithWord> ElementsDict { get; set; }
        private Dictionary<string, RectangleWithWord> PositionedElementsDict { get; }



        public CircularCloudLayouterForRectanglesWithText(Point center, Func<List<RectangleWithWord>, IVisualization> visualization)
        {
            Center = center;
            LayouterSpiral = new Spiral();
            ElementsDict = new Dictionary<string, RectangleWithWord>();
            PositionedElementsDict = new Dictionary<string, RectangleWithWord>();
            Visualization = visualization;
        }

        
        private void PutNextElement(RectangleWithWord rectangleWithWord)
        {
            var rectangleSize = rectangleWithWord.RectangleElement.Size;
            var word = rectangleWithWord.WordElement;

            if (rectangleSize.Width == 0 || rectangleSize.Height == 0)
                throw new ArgumentException();

            if (ElementsDict.ContainsKey(word.WordText))
            {
                ElementsDict[word.WordText].WordElement.CntOfWords++;

                ElementsDict[word.WordText].ChangeSizeOfField(Visualization.Invoke(ElementsDict.Values.ToList()));

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
        
        
        /*
        private void PutNextElement(RectangleWithWord rectangleWithWord)
        {
            if (rectangleWithWord.RectangleElement.Width == 0 
                || rectangleWithWord.RectangleElement.Height == 0)
                throw new ArgumentException();

            var text = rectangleWithWord.WordElement.WordText;

            if (ElementsDict.ContainsKey(text))
            {
                ElementsDict[text].WordElement.CntOfWords++;
                ElementsDict[text].ChangeSizeOfField();    //ЗДЕСЬ ИЗМЕНИТЬ АДЕКВАТНО РАЗМЕР
            }
            else
            {
                ElementsDict[text] = rectangleWithWord;
            }
        }
        */
        

        //public RectangleWithWord

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
            /*
            
            foreach (var word in wordList)
            {
                var element = CreateNewRectangleWithWord(elementSize, word); //!!!

                PutNextElement(element);
            }

            foreach (var key in ElementsDict.Keys)
            {
                var value = ElementsDict[key];

                var positionedElement = MakePositionForElement(value);

                PositionedElementsDict[key] = positionedElement;

            }
            ElementsDict = PositionedElementsDict;
            */

            
            foreach (var word in wordList)
            {
                var element = CreateNewRectangleWithWord(elementSize, word); //!!!

                PutNextElement(element);
            }
            

        }

        private RectangleWithWord MakePositionForElement(RectangleWithWord inputElement)
        {
            var rectangleSize = inputElement.RectangleElement.Size;
            //var nextElement = CreateNewRectangleWithWord(rectangleSize, inputElement.WordElement);
            var nextElement = inputElement;

            while (PositionedElementsDict.Values.Any(el => el.RectangleElement.IntersectsWith(nextElement.RectangleElement)))
                nextElement = CreateNewRectangleWithWord(rectangleSize, inputElement.WordElement);
            if (nextElement.RectangleElement.Location != Center)
                nextElement = CenterElement(nextElement);


            return nextElement;
        }

        private RectangleWithWord CenterElement(RectangleWithWord inputRectangleWithWord)
        {
            var centeringRectangleElement = ElementCentering.Centering(inputRectangleWithWord.RectangleElement, Center, ElementsDict);
            inputRectangleWithWord.RectangleElement = centeringRectangleElement;
            return inputRectangleWithWord;
        }

        /*
        private bool IsIntersect(Rectangle inputRectangle) =>
            ElementsDict.Select(el => el.Value)
                .Any(rect => rect.RectangleElement.IntersectsWith(inputRectangle));
        */
    }
}

using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualizationDI;
using TagsCloudVisualizationDI.Layouter.Filler;
using TagsCloudVisualizationDI.TextAnalization.Visualization;

namespace TagsCloudVisualizationDITests
{
    [TestFixture]
    public class ComplexFormingTests
    {
        private Dictionary<string, RectangleWithWord> _formedElements;
        private List<RectangleWithWord> _sizedElements;
        private List<RectangleWithWord> _positionedElements;
        private Rectangle _defaultRectangle;
        private Dictionary<string, RectangleWithWord> _expectedFormedElements;
        private List<Size> _expectedSizes;
        private List<Point> _expectedPositiones;



        [SetUp]
        public void SetUpValues()
        {
            var center = new Point(0, 0);
            var filler = new CircularCloudLayouterForRectanglesWithText(center);
            var elementSize = new Size(100, 200);
            var brush = new SolidBrush(Color.Black);
            var font = new Font("Times", 15);
            var normalyzedWords = new List<Word>();
            _defaultRectangle = new Rectangle(center, elementSize);

            var inputStrings = new List<string>
            {
                "альфа",
                "альфа",
                "бета",
                "гамма",
            };

            foreach (var str in inputStrings)
            {
                normalyzedWords.Add(new Word(str));
            }

            var expectedFormedElements = MakeExpectedFormedElements();
            _expectedFormedElements = expectedFormedElements;


            var visualization = new DefaultVisualization(brush, font, elementSize, 25);

            var formedElements = filler.FormStatisticElements(elementSize, normalyzedWords);
            var sizedElements = visualization.FindSizeForElements(formedElements);
            var sortedElements = sizedElements.
                OrderByDescending(el => el.WordElement.CntOfWords).ToList();
            var positionedElements = filler.MakePositionElements(sortedElements);
            _expectedSizes = new List<Size>
            {
                new Size(193, 82),
                new Size(74, 41),
                new Size(99, 41),
            };
            _expectedPositiones = new List<Point>()
            {
                new Point(-96, -41),
                new Point(-16, 41),
                new Point(-76, -82),
            };
            _formedElements = formedElements;
            _sizedElements = sizedElements;
            _positionedElements = positionedElements;
        }

        [Test]
        public void ElementsShouldBeCorrectlyFormed()
        {
            _formedElements.Select(el => el.Value.WordElement.CntOfWords)
                .Should().BeEquivalentTo(_expectedFormedElements.Select(el => el.Value.WordElement.CntOfWords));
        }

        [Test]
        public void ElementsShouldBeCorrectlySized()
        {
            _sizedElements.Select(el => el.RectangleElement.Size).ToList()
                .Should().BeEquivalentTo(_expectedSizes);
        }

        [Test]
        public void ElementsShouldBeCorrectlyPlaced()
        {
            _positionedElements.Select(el => el.RectangleElement.Location)
                .Should().BeEquivalentTo(_expectedPositiones);
        }

        private Dictionary<string, RectangleWithWord> MakeExpectedFormedElements()
        {
            var expectedFormedElements = new Dictionary<string, RectangleWithWord>
            {
                ["альфа"] = new RectangleWithWord(_defaultRectangle, new Word("альфа")),
                ["бета"] = new RectangleWithWord(_defaultRectangle, new Word("бета")),
                ["гамма"] = new RectangleWithWord(_defaultRectangle, new Word("гамма")),
            };
            expectedFormedElements["альфа"].WordElement.CntOfWords = 2;
            expectedFormedElements["бета"].WordElement.CntOfWords = 1;
            expectedFormedElements["гамма"].WordElement.CntOfWords = 1;
            return expectedFormedElements;
        }
    }
}

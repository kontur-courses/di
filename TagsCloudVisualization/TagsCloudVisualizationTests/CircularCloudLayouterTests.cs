using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FluentAssertions;
using FluentAssertions.Common;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests
{
    [TestFixture]
    class CircularCloudLayouterTests
    {
        private CircularCloudLayouter layouter;
        private Point center;
        private Random random;
        private List<GraphicWord> countedWords;
        private WordCounter counter = new WordCounter();

        [SetUp]
        public void SetUp()
        {
            countedWords = new List<GraphicWord>();
            center = new Point(400, 400);
            layouter = new CircularCloudLayouter(new LinearSizer());
            random = new Random(1);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
                return;

            var visualizer = new Visualizer();
            var image = visualizer.Render(countedWords, 800, 800, new MonochromePalette(Color.Black, Color.White));
            ImageSaver.WriteToFile(TestContext.CurrentContext.Test.Name, image);
        }

        [TestCase(0, 0, 10, 6, Description = "Zero center")]
        [TestCase(-5, 4, 10, 6, Description = "Non zero center")]
        [TestCase(2, 2, 7, 3, Description = "Odd width and height")]
        public void PutFirstRectangle_InCenter(int x, int y, int width, int height)
        {
            var word = new GraphicWord("word");
            word.Rectangle = new Rectangle(x, y, width, height);
            layouter.Process(new[] {word}, new LinearSizer(), center);
            word.Rectangle.Location
                .IsSameOrEqualTo(new Point(x - width / 2, y - height / 2));
        }

        [TestCase(new[] { 5, 5, 5, 5 }, new[] { -5, -5, 5, 5 }, ExpectedResult = false)]
        [TestCase(new[] { 0, 0, 10, 10 }, new[] { 2, 2, 5, 5 }, ExpectedResult = true, Description = "Internal rectangle")]
        [TestCase(new[] { 0, 0, 10, 10 }, new[] { -2, -2, 5, 5 }, ExpectedResult = true)]
        [TestCase(new[] { -2, -2, 5, 5 }, new[] { 0, 0, 10, 10 }, ExpectedResult = true)]
        [TestCase(new[] { 0, 0, 10, 10 }, new[] { 0, 20, 10, 10 }, ExpectedResult = false, Description = "Y-Axis is parallel")]
        [TestCase(new[] { 0, 0, 10, 10 }, new[] { -10, 20, 10, 10 }, ExpectedResult = false)]
        public bool CheckCollision_BetweenTwoRectangles(int[] rect, int[] other)
        {
            return Geometry.IsRectangleIntersection(
                new Rectangle(rect[0], rect[1], rect[2], rect[3]),
                new Rectangle(other[0], other[1], other[2], other[3]));
        }

        [TestCase(10, 10, Description = "Equal length")]
        [TestCase(3, 10)]
        public void CloudOccupiesMoreThanHalfOfCircleArea(int minLength, int maxLength)
        {
            countedWords = GetWords(minLength, maxLength);
            var maxRadius = 0.0;
            var occupiedArea = 0.0;
            foreach (var word in countedWords)
            {
                occupiedArea += word.Rectangle.Width * word.Rectangle.Height;
                var radius = Geometry.GetMaxDistanceToRectangle(center, word.Rectangle);
                if (radius > maxRadius)
                    maxRadius = radius;
            }

            (occupiedArea / (maxRadius * maxRadius * Math.PI)).Should().BeGreaterThan(0.5);
        }

        [TestCase(10, 10, Description = "Equal length")]
        [TestCase(3, 10)]
        public void ResultRectanglesDoNotIntersect(int minLength, int maxLength)
        {
            countedWords = GetWords(minLength, maxLength);
            var rest = countedWords;
            foreach (var word in countedWords)
            {
                rest = rest.Skip(1).ToList();
                foreach (var other in rest)
                {
                    Geometry.IsRectangleIntersection(word.Rectangle, other.Rectangle).Should().BeFalse();
                }
            }
        }

        private List<GraphicWord> GetWords(int minLength, int maxLength)
        {
            var chars = "aaabbcdefghtf";
            var rawString = new StringBuilder();
            for (var i = 0; i < 100; i++)
            {
                rawString.Append(" " + new string(chars[random.Next(chars.Length)], random.Next(minLength, maxLength)));
            }

            countedWords = counter.Count(false, rawString.ToString());
            layouter.Process(countedWords, new LinearSizer(), center);
            return countedWords;
        }
    }
}

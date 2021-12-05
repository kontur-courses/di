using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization;
using TagsCloudVisualization.Layouter;


namespace TagsCloudVisualizationTests
{
    
    [TestFixture]
    public class TestsCircularCloudLayouterShouldCorrectPutNext
    {
        private List<RectangleWithWord> _rectanglesWithNamesList = new List<RectangleWithWord>(); 
        
        [TearDown]
        public void VisualizeError()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure && _rectanglesWithNamesList.Count != 0)
            {
                using (var visualization = new Visualization(_rectanglesWithNamesList, new Pen(Color.White, 3), 
                    new SolidBrush(Color.White), new Font("Times", 15)))
                {
                    var testName = TestContext.CurrentContext.Test.Name;
                    var path = AppDomain.CurrentDomain.BaseDirectory + testName + "." + ImageFormat.Jpeg;
                    Console.WriteLine($"Tag cloud visualization saved to file {path}");
                    visualization.DrawAndSaveImage(new Size(5000, 5000), path, ImageFormat.Jpeg);
                }
            }
            _rectanglesWithNamesList = new List<RectangleWithWord>();
        }


        [TestCase(0, 1)]
        [TestCase(1,0)]
        [TestCase(0, 0)]
        public void ShouldThrowExceptionWhenPutRectangleWithZeroEdge(int width, int height)
        {

            var rectangleSize = new Size(width, height);
            var layouterCenter = new Point(2500, 2500);
            var layouter = new CircularCloudLayouterForRectanglesWithText(layouterCenter);
            Action put = () => layouter.PutNextElement(rectangleSize, new Word(""));
            put.Should().Throw<ArgumentException>();
        }

        [TestCase(-5, 10)]
        [TestCase(5, -5)]
        [TestCase(10, 10)]
        public void ShouldNotThrowIfPutRectangleWithCorrectSize(int width, int height)
        {
            var rectangleSize = new Size(width, height);
            var layouterCenter = new Point(2500, 2500);
            var layouter = new CircularCloudLayouterForRectanglesWithText(layouterCenter);
            Action put = () => layouter.PutNextElement(rectangleSize, new Word(""));
            put.Should().NotThrow();
        }

        [TestCase(20, 10)]
        [TestCase(20, -10)]
        [TestCase(-15, -5)]
        [TestCase(-15, 5)]
        public void ShouldNotIntersectWithSameRectangles(int width, int height)
        {
            var layouterCenter = new Point(width, height);
            var layouter = new CircularCloudLayouterForRectanglesWithText(layouterCenter);
            var rectangleSize = new Size(50, 100);
            for (int i = 0; i < 2; i++)
            {
                _rectanglesWithNamesList.Add(layouter.PutNextElement(rectangleSize, new Word("")));
            }
            foreach (var rectangle in _rectanglesWithNamesList)
            {
                var z = _rectanglesWithNamesList
                    .Where(r => r.RectangleElement != rectangle.RectangleElement)
                    .Where(r => r.RectangleElement.IntersectsWith(rectangle.RectangleElement));

                var act = _rectanglesWithNamesList
                    .Where(r => r != rectangle)
                    .Any(r => r.RectangleElement.IntersectsWith(rectangle.RectangleElement));
                act.Should().BeFalse();
            }
        }

        [Test]
        public void SingleRectangleInCenterPutCorrectly()
        {

            var rectangleSize = new Size(50, 60);
            var layouterCenter = new Point(2500,2500);
            var layouter = new CircularCloudLayouterForRectanglesWithText(layouterCenter);
            var rectangle = layouter.PutNextElement(rectangleSize, new Word(""));
            rectangle.RectangleElement.Location.Should().Be(layouterCenter);
        }

        [TestCase(300, 978043567)]
        [TestCase(500, 984576457)]
        [TestCase(1000, 756845767)]
        public void FormShouldBeCloserToCircleThanToSquareWhenManyRectangles(int number, int seed)
        {
            var seedRandom = new Random(seed);
            Size rectangleSize = new Size(Point.Empty);
            var layouterCenter = new Point(2500, 2500);
            var layouter = new CircularCloudLayouterForRectanglesWithText(layouterCenter);
            for (int i = 0; i < number; i++)
            {
                while (rectangleSize.Height == 0 || rectangleSize.Width == 0)
                    rectangleSize = new Size(seedRandom.Next(-60, 60), seedRandom.Next(-60, 60));
                _rectanglesWithNamesList.Add(layouter.PutNextElement(rectangleSize, new Word("")));
            }

            var listOfRectangles = _rectanglesWithNamesList.Select(r => r.RectangleElement).ToList();

            var sumArea = GetSumAreaOfRectangles(_rectanglesWithNamesList.Select(r => r.RectangleElement).ToList());
            var circleArea = GetCircleArea(GetCircleRadius(layouterCenter, listOfRectangles));
            
            var enclosingRectangleArea = GetEnclosingRectangleArea(listOfRectangles);
            var difCircleAndSum = sumArea/circleArea;
            var difSumAndEnclosingRectangle = sumArea/enclosingRectangleArea;

            difCircleAndSum.Should().BeGreaterThan(difSumAndEnclosingRectangle);
        }

        [Test]
        public void RectanglesShouldNotIntersect()
        {
            var seedRandom = new Random(546748576);
            var rectangleSize = new Size(Point.Empty);
            var layouterCenter = new Point(2500, 2500);
            var layouter = new CircularCloudLayouterForRectanglesWithText(layouterCenter);
            for (int i = 0; i < 300; i++)
            {
                while(rectangleSize.Width == 0|| rectangleSize.Height == 0)
                    rectangleSize = new Size(seedRandom.Next(-100, 100), seedRandom.Next(-100, 100));
                _rectanglesWithNamesList.Add(layouter.PutNextElement(rectangleSize, new Word("")));
            }

            foreach (var rectangle in _rectanglesWithNamesList)
            {
                var act = _rectanglesWithNamesList
                    .Where(r => r != rectangle)
                    .Any(r => r.RectangleElement.IntersectsWith(rectangle.RectangleElement));
                act.Should().BeFalse();
            }
        }

        private double GetCircleRadius(Point layouterCenter, List<Rectangle> rectangles)
        {
            double maxRadius = 0;
            foreach (var rectangle in rectangles)
            {
                foreach (var node in rectangle.GetRectangleNodes())
                {
                    maxRadius = Math.Max(maxRadius, node.GetDistanceToPoint(layouterCenter));
                }
            }
            return maxRadius;
        }

        private double GetSumAreaOfRectangles(List<Rectangle> rectangles)
        {
            double result = 0;
            foreach (var rectangle in rectangles)
                result += Math.Abs(rectangle.Height) * Math.Abs(rectangle.Width);
            return result;
        }


        private double GetEnclosingRectangleArea(List<Rectangle> rectangles)
        {
            var xMax = int.MinValue;
            var xMin = int.MaxValue;
            var yMin = int.MaxValue;
            var yMax = int.MinValue;
            foreach (var rectangle in rectangles)
            {
                foreach (var node in rectangle.GetRectangleNodes())
                {
                    if (node.X > xMax)
                        xMax = node.X;
                    if (node.Y > yMax)
                        yMax = node.Y;
                    if (node.X < xMin)
                        xMin = node.X;
                    if (node.Y < yMin)
                        yMin = node.Y;
                }
            }
            return (xMax - xMin) * (yMax - yMin);
        }

        private double GetCircleArea(double circleRadius)
        {
            if (circleRadius <= 0)
                throw new ArgumentException();
            var area = Math.PI * Math.Pow(circleRadius, 2);
            return area;
        }
    }
}


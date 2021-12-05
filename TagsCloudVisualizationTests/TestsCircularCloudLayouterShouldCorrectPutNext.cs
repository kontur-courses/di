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
    /*
    [TestFixture]
    public class TestsCircularCloudLayouterShouldCorrectPutNext
    {
        private List<Rectangle> _rectanglesList = new List<Rectangle>(); 
        
        [TearDown]
        public void VisualizeError()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure && _rectanglesList.Count != 0)
            {
                using (var visualization = new Visualization(_rectanglesList, new Pen(Color.White, 3)))
                {
                    var testName = TestContext.CurrentContext.Test.Name;
                    var path = AppDomain.CurrentDomain.BaseDirectory + testName + "." + ImageFormat.Jpeg;
                    Console.WriteLine($"Tag cloud visualization saved to file {path}");
                    visualization.DrawAndSaveImage(new Size(5000, 5000), path, ImageFormat.Jpeg);
                }
            }
            _rectanglesList = new List<Rectangle>();
        }


        [TestCase(0, 1)]
        [TestCase(1,0)]
        [TestCase(0, 0)]
        public void ShouldThrowExceptionWhenPutRectangleWithZeroEdge(int width, int height)
        {

            var rectangleSize = new Size(width, height);
            var layouterCenter = new Point(2500, 2500);
            var layouter = new CircularCloudLayouterForRectangles(layouterCenter);
            Action put = () => layouter.PutNextRectangle(rectangleSize);
            put.Should().Throw<ArgumentException>();
        }

        [TestCase(-5, 10)]
        [TestCase(5, -5)]
        [TestCase(10, 10)]
        public void ShouldNotThrowIfPutRectangleWithCorrectSize(int width, int height)
        {
            var rectangleSize = new Size(width, height);
            var layouterCenter = new Point(2500, 2500);
            var layouter = new CircularCloudLayouterForRectangles(layouterCenter);
            Action put = () =>layouter.PutNextRectangle(rectangleSize);
            put.Should().NotThrow();
        }

        [TestCase(20, 10)]
        [TestCase(20, -10)]
        [TestCase(-15, -5)]
        [TestCase(-15, 5)]
        public void ShouldNotIntersectWithSameRectangles(int width, int height)
        {
            var layouterCenter = new Point(width, height);
            var layouter = new CircularCloudLayouterForRectangles(layouterCenter);
            var rectangleSize = new Size(50, 100);
            for (int i = 0; i < 300; i++)
            {
                _rectanglesList.Add(layouter.PutNextRectangle(rectangleSize));
            }
            foreach (var rectangle in _rectanglesList)
            {
                var act = _rectanglesList
                    .Where(r => r != rectangle)
                    .Any(r => r.IntersectsWith(rectangle));
                act.Should().BeFalse();
            }
        }

        [Test]
        public void SingleRectangleInCenterPutCorrectly()
        {

            var rectangleSize = new Size(50, 60);
            var layouterCenter = new Point(2500,2500);
            var layouter = new CircularCloudLayouterForRectangles(layouterCenter);
            var rectangle = layouter.PutNextRectangle(rectangleSize);
            rectangle.Location.Should().Be(layouterCenter);
        }

        [TestCase(300, 978043567)]
        [TestCase(500, 984576457)]
        [TestCase(1000, 756845767)]
        public void FormShouldBeCloserToCircleThanToSquareWhenManyRectangles(int number, int seed)
        {
            var seedRandom = new Random(seed);
            Size rectangleSize = new Size(Point.Empty);
            var layouterCenter = new Point(2500, 2500);
            var layouter = new CircularCloudLayouterForRectangles(layouterCenter);
            for (int i = 0; i < number; i++)
            {
                while (rectangleSize.Height == 0 || rectangleSize.Width == 0)
                    rectangleSize = new Size(seedRandom.Next(-60, 60), seedRandom.Next(-60, 60));
                _rectanglesList.Add(layouter.PutNextRectangle(rectangleSize));
            }
            var sumArea = GetSumAreaOfRectangles(_rectanglesList);
            var circleArea = GetCircleArea(GetCircleRadius(layouterCenter, _rectanglesList));
            
            var enclosingRectangleArea = GetEnclosingRectangleArea(_rectanglesList);
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
            var layouter = new CircularCloudLayouterForRectangles(layouterCenter);
            for (int i = 0; i < 300; i++)
            {
                while(rectangleSize.Width == 0|| rectangleSize.Height == 0)
                    rectangleSize = new Size(seedRandom.Next(-100, 100), seedRandom.Next(-100, 100));
                _rectanglesList.Add(layouter.PutNextRectangle(rectangleSize));
            }

            foreach (var rectangle in _rectanglesList)
            {
                var act = _rectanglesList
                    .Where(r => r != rectangle)
                    .Any(r => r.IntersectsWith(rectangle));
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
    */
}

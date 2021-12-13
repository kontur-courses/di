using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using NUnit.Framework.Interfaces;
using TagsCloudVisualizationDI;
using TagsCloudVisualizationDI.TextAnalization.Visualization;


namespace TagsCloudVisualizationTests
{
    
    [TestFixture]
    public class TestsCircularCloudLayouterShouldCorrectPutNext
    {
        private List<RectangleWithWord> _rectanglesWithNamesList = new List<RectangleWithWord>(); 
        
        /*
        [TearDown]
        public void VisualizeError()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure && _rectanglesWithNamesList.Count != 0)
            {
                var testName = TestContext.CurrentContext.Test.Name;
                var path = AppDomain.CurrentDomain.BaseDirectory + testName + "." + ImageFormat.Jpeg;
                //using (var visualization = new TagsCloudVisualizationDI.TextAnalization.Visualization.(_rectanglesWithNamesList, new Pen(Color.White, 3), 
                //new SolidBrush(Color.White), new Font("Times", 15)))
                using (var visualization = new DefaultVisualization(_rectanglesWithNamesList, path, new SolidBrush(Color.White),
                    new Font("Times", 15), ImageFormat.Png, new Size(5000, 5000)))
                {
                    Console.WriteLine($"Tag cloud visualization saved to file {path}");
                    visualization.DrawAndSaveImage();
                }
            }
            _rectanglesWithNamesList = new List<RectangleWithWord>();
        }
        */

        /*
        [TestCase(0, 1)]
        [TestCase(1,0)]
        [TestCase(0, 0)]
        public void ShouldThrowExceptionWhenPutRectangleWithZeroEdge(int width, int height)
        {

            var rectangleSize = new Size(width, height);
            var layouterCenter = new Point(2500, 2500);
            var layouter = new CircularCloudLayouterForRectanglesWithText(layouterCenter);
            var rectangle = RectangleWithWord.MakeFakeWordRectangle(rectangleSize);
            Action put = () => layouter.PutNextElement(rectangle);
            put.Should().Throw<ArgumentException>();
        }
        */
        /*
        [TestCase(-5, 10)]
        [TestCase(5, -5)]
        [TestCase(10, 10)]
        public void ShouldNotThrowIfPutRectangleWithCorrectSize(int width, int height)
        {
            var rectangleSize = new Size(width, height);
            var layouterCenter = new Point(2500, 2500);
            var layouter = new CircularCloudLayouterForRectanglesWithText(layouterCenter);
            var rectangle = RectangleWithWord.MakeFakeWordRectangle(rectangleSize);
            Action put = () => layouter.PutNextElement(rectangle);
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
                var rectangle = RectangleWithWord.MakeFakeWordRectangle(rectangleSize);
                _rectanglesWithNamesList.Add(layouter.PutNextElement(rectangle));
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
            var rectangle = RectangleWithWord.MakeFakeWordRectangle(rectangleSize);
            rectangle = layouter.PutNextElement(rectangle);
            rectangle.RectangleElement.Location.Should().Be(layouterCenter);
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
                var rectangle = RectangleWithWord.MakeFakeWordRectangle(rectangleSize);
                _rectanglesWithNamesList.Add(layouter.PutNextElement(rectangle));
            }

            foreach (var rectangle in _rectanglesWithNamesList)
            {
                var act = _rectanglesWithNamesList
                    .Where(r => r != rectangle)
                    .Any(r => r.RectangleElement.IntersectsWith(rectangle.RectangleElement));
                act.Should().BeFalse();
            }
        }
        */




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


using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloud;
using TagCloud.ColorPicker;
using TagCloud.Drawer;
using TagCloud.RectanglesLayouter;
using TagCloud.RectanglesLayouter.PointsGenerator;

namespace TagCloudTests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private const int DistanceBetweenPoints = 1;
        private const double AngleIncrement = 0.01;

        private CircularCloudLayouter layouter;
        private Point center;
        private Size rectangleSize;
        private IPointsGenerator pointsGenerator;

        private static IEnumerable RectanglesAmountTestCases
        {
            get
            {
                yield return new TestCaseData(1).SetName("OneRectangle");
                yield return new TestCaseData(2).SetName("TwoRectangles");
                yield return new TestCaseData(1000).SetName("ThousandRectangles");
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
                return;
            var fileName = $"{TestContext.CurrentContext.Test.Name}TestLayout.png";
            var path = $"{TestContext.CurrentContext.WorkDirectory}/{fileName}";
            new CloudDrawer(new ColorPicker()).CreateImage(layouter.Rectangles).Save(fileName);
            Console.WriteLine($"Tag cloud visualization saved to file {path}");
        }

        [Test]
        public void Constructor_WithCenterAndGenerator_DoesNotThrowException()
        {
            center = new Point(5, 5);
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);

            Action action = () => new CircularCloudLayouter(center, pointsGenerator);

            action.Should().NotThrow();
        }

        [Test]
        public void PutNextRectangleMethod_OnCorrectSize_DoesNotThrowException()
        {
            center = new Point(5, 5);
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);
            layouter = new CircularCloudLayouter(center, pointsGenerator);
            rectangleSize = new Size(10, 10);

            Action action = () => layouter.PutNextRectangle(rectangleSize);

            action.Should().NotThrow();
        }

        [Test]
        public void Rectangles_AfterLayouterCreating_IsEmptyList()
        {
            center = new Point(5, 5);
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);
            layouter = new CircularCloudLayouter(center, pointsGenerator);

            var rectangles = layouter.Rectangles;

            rectangles.Should().BeEmpty();
        }

        [TestCaseSource(nameof(RectanglesAmountTestCases))]
        public void Rectangles_AfterAdding_HasAllRectanglesInList(int rectangleAmount)
        {
            center = new Point(5, 5);
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);
            layouter = new CircularCloudLayouter(center, pointsGenerator);
            rectangleSize = new Size(10, 10);

            AddRectangles(rectangleAmount);
            var rectanglesAmount = layouter.Rectangles.Count;

            rectanglesAmount.Should().Be(rectangleAmount);
        }

        [TestCase(0, 1, TestName = "WidthIsZero")]
        [TestCase(1, 0, TestName = "HeightIsZero")]
        [TestCase(-1, 0, TestName = "WidthIsNegative")]
        [TestCase(0, -1, TestName = "HeightIsNegative")]
        [TestCase(0, 0, TestName = "BothDimensionsAreZero")]
        [TestCase(-1, -1, TestName = "BothDimensionsAreNegative")]
        public void PutNextRectangle_OnInvalidSize_ThrowsArgumentException(int width, int height)
        {
            center = new Point(5, 5);
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);
            layouter = new CircularCloudLayouter(center, pointsGenerator);
            var invalidRectangleSize = new Size(width, height);

            Action action = () => layouter.PutNextRectangle(invalidRectangleSize);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutNextRectangle_AfterFirstAdding_ReturnsRectangleInCenter()
        {
            center = new Point(5, 5);
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);
            layouter = new CircularCloudLayouter(center, pointsGenerator);
            rectangleSize = new Size(10, 10);

            var firstRectangle = layouter.PutNextRectangle(rectangleSize);
            var isRectangleInCenter = firstRectangle.Contains(center);

            isRectangleInCenter.Should().BeTrue();
        }

        [Test]
        public void PutNextRectangle_AfterFirstAdding_ReturnsRectangleWithCenterInLayoutCenter()
        {
            center = new Point(5, 5);
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);
            layouter = new CircularCloudLayouter(center, pointsGenerator);
            rectangleSize = new Size(10, 10);

            var firstRectangle = layouter.PutNextRectangle(rectangleSize);
            var firstRectangleCenter = firstRectangle.GetCenter();

            firstRectangleCenter.Should().Be(center);
        }

        [TestCaseSource(nameof(RectanglesAmountTestCases))]
        public void Rectangles_WithSameSize_DoNotIntersect(int rectanglesAmount)
        {
            center = new Point(5, 5);
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);
            layouter = new CircularCloudLayouter(center, pointsGenerator);
            rectangleSize = new Size(10, 10);

            AddRectangles(rectanglesAmount);

            CheckIntersection(layouter.Rectangles);
        }

        [TestCaseSource(nameof(RectanglesAmountTestCases))]
        public void Rectangles_WithRandomSize_DoNotIntersect(int rectanglesAmount)
        {
            center = new Point(5, 5);
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);
            layouter = new CircularCloudLayouter(center, pointsGenerator);

            AddRandomRectangles(rectanglesAmount);

            CheckIntersection(layouter.Rectangles);
        }

        [TestCase(100, TestName = "HundredRectangles")]
        [TestCase(200, TestName = "TwoHundredRectangles")]
        [TestCase(500, TestName = "FiveHundredRectangles")]
        public void Rectangles_OnIncreasingSize_HaveDensityMoreThanSeventyPercent(int rectanglesAmount)
        {
            const double expectedDensity = 0.7;
            center = new Point(5, 5);
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);
            layouter = new CircularCloudLayouter(center, pointsGenerator);

            for (var i = rectanglesAmount; i > 0; i--)
                layouter.PutNextRectangle(new Size(i * 3, i));
            var radius = GetDistanceToFatherPoint(layouter.Rectangles);
            var circleSquare = Math.PI * radius * radius;
            var rectanglesSquare = layouter.Rectangles.Sum(rectangle => rectangle.Width * rectangle.Height);
            var density = rectanglesSquare / circleSquare;

            density.Should().BeGreaterOrEqualTo(expectedDensity);
        }

        [Test]
        public void PutNextRectangle_UsesPointsGenerator()
        {
            center = new Point(5, 5);
            pointsGenerator = A.Fake<IPointsGenerator>();
            layouter = new CircularCloudLayouter(center, pointsGenerator);

            layouter.PutNextRectangle(new Size(10, 10));

            A.CallTo(() => pointsGenerator.GetPoints()).MustHaveHappened();
        }

        [Test]
        public void PutNextRectangle_UsesPointsGeneratorOnce()
        {
            center = new Point(5, 5);
            pointsGenerator = A.Fake<IPointsGenerator>();
            A.CallTo(() => pointsGenerator.GetPoints()).Returns(new[] {new Point(), new Point(100, 100)});
            layouter = new CircularCloudLayouter(center, pointsGenerator);

            layouter.PutNextRectangle(new Size(10, 10));
            layouter.PutNextRectangle(new Size(10, 10));

            A.CallTo(() => pointsGenerator.GetPoints()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void PutNextRectangle_UsesPointsFromPointsGenerator()
        {
            var points = new[] {new Point(), new Point(2, 0), new Point(0, 2)};
            center = new Point();
            pointsGenerator = A.Fake<IPointsGenerator>();
            A.CallTo(() => pointsGenerator.GetPoints()).Returns(points);
            layouter = new CircularCloudLayouter(center, pointsGenerator);

            layouter.PutNextRectangle(new Size(1, 1));
            layouter.PutNextRectangle(new Size(1, 1));
            layouter.PutNextRectangle(new Size(1, 1));

            layouter.Rectangles.Select(rectangle => rectangle.GetCenter()).Should().BeEquivalentTo(points);
        }

        private double GetDistanceToFatherPoint(IEnumerable<Rectangle> rectangles)
        {
            var maxDistance = double.MinValue;
            foreach (var rectangle in rectangles)
            {
                foreach (var corner in rectangle.GetCorners())
                {
                    var distance = GetDistance(center, corner);
                    if (distance > maxDistance)
                        maxDistance = distance;
                }
            }
            return maxDistance;
        }

        private double GetDistance(Point firstPoint, Point secondPoint) =>
            Math.Sqrt(Math.Pow(firstPoint.X + secondPoint.X, 2) + Math.Pow(firstPoint.Y + secondPoint.Y, 2));

        private void AddRectangles(int rectanglesAmount)
        {
            for (var i = 0; i < rectanglesAmount; i++)
                layouter.PutNextRectangle(rectangleSize);
        }

        private void AddRandomRectangles(int rectanglesAmount)
        {
            var random = new Random();
            for (var i = 0; i < rectanglesAmount; i++)
            {
                var width = random.Next(1, 10);
                var height = random.Next(1, 10);
                var size = new Size(width, height);
                layouter.PutNextRectangle(size);
            }
        }

        private void CheckIntersection(List<Rectangle> rectangles)
        {
            for (var i = 0; i < rectangles.Count; i++)
            {
                for (var j = i + 1; j < rectangles.Count; j++)
                    RectanglesIntersect(rectangles[i], rectangles[j]).Should().BeFalse();
            }
        }

        private bool RectanglesIntersect(Rectangle firstRectangle, Rectangle secondRectangle)
        {
            return secondRectangle.Left < firstRectangle.Right &&
                   firstRectangle.Left < secondRectangle.Right &&
                   secondRectangle.Top < firstRectangle.Bottom &&
                   firstRectangle.Top < secondRectangle.Bottom;
        }
    }
}
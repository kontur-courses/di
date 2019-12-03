using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.Exceptions;
using TagsCloudVisualization.Geometry;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.TagCloudLayouters;
using TagsCloudVisualization.TagsCloudVisualization;

namespace TagCloudVisualizationTests.TagCloudLayouters
{
    [TestFixture]
    class CircularCloudLayouterShould
    {
        private CircularCloudLayouter circularCloudLayouter;
        private List<Rectangle> rectangles = new List<Rectangle>();
        private Point center;
        private readonly Size[] sizesForTesting = {
            new Size(2, 4),
            new Size(3, 4),
            new Size(2, 2),
            new Size(3, 3),
            new Size(1, 1),
            new Size(2, 2),
            new Size(3, 2)
        };
        private readonly Point[] expectedLocations = {
            new Point(299, 298),
            new Point(296, 298),
            new Point(297, 296),
            new Point(299, 295),
            new Point(301, 298),
            new Point(301, 299),
            new Point(301, 301)
        };

        [SetUp]
        public void SetUp()
        {
            center = new Point(300, 300);
            circularCloudLayouter = new CircularCloudLayouter(new CloudSettings(center, 10000));
        }

        [Test]
        public void LocateTopLeftCornerOfFirstRectangleInCenter()
        {
            var size = new Size(2, 4);
            circularCloudLayouter.PutNextRectangle(size).Location.Should().Be(new Point(299, 298));
        }

        [Test]
        public void ReturnPositionClosestToCenter()
        {
            sizesForTesting
                .Take(4)
                .Select(size => circularCloudLayouter.PutNextRectangle(size).Location)
                .Should()
                .BeEquivalentTo(expectedLocations.Take(4));
        }

        [TestCase(-1, 3, TestName = "When_WidthLessThanZero")]
        [TestCase(32, -3, TestName = "When_HeightLessThanZero")]
        public void ThrowArgumentException(int width, int height)
        {
            Func<Rectangle> act = () => circularCloudLayouter.PutNextRectangle(new Size(width, height));
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ThrowOutOfPermissibleRangeException_When_RectangleIsOutPermissibleRange()
        {
            Func<Rectangle> act = () => circularCloudLayouter.PutNextRectangle(new Size(30000, 4000));
            act.Should().Throw<OutOfPermissibleRangeException>();
        }

        [Test]
        public void CreateDensityCloud()
        {
            rectangles = sizesForTesting.Select(size => circularCloudLayouter.PutNextRectangle(size)).ToList();
            double allRectanglesSquare = rectangles.Select(rectangle => rectangle.Square()).Sum();
            var corners = GetAllPoints();
            var radiuses = GetFourRadiuses(corners).ToList();
            var radius = radiuses.Sum() / 4;
            var circleSquare = Math.PI * radius * radius;
            allRectanglesSquare.Should().BeGreaterOrEqualTo(80 * circleSquare / 100);
        }

        private HashSet<Point> GetAllPoints()
        {
           return rectangles.SelectMany(rectangle => rectangle.GetCorners()).ToHashSet();
        }

        private IEnumerable<double> GetFourRadiuses(HashSet<Point> allCirclePoints)
        {
            yield return Math.Abs(allCirclePoints.Min(point => point.X) - center.X);
            yield return Math.Abs(allCirclePoints.Max(point => point.X) - center.X);
            yield return Math.Abs(allCirclePoints.Min(point => point.Y) - center.Y);
            yield return Math.Abs(allCirclePoints.Max(point => point.Y) - center.Y);
        }

        [TestCase(50)]
        [TestCase(150)]
        [TestCase(700)]
        public void CreateCloud_WithCircleShape(int rectanglesCount)
        {
            var random = new Random();
            for (var i = 0; i < rectanglesCount; i++)
                rectangles.Add(circularCloudLayouter.PutNextRectangle(new Size(random.Next(1, 50), random.Next(1, 50))));
            var corners = GetAllPoints();
            var radiuses = GetFourRadiuses(corners).ToList();
            var middleRadius = radiuses.Sum() / 4;
            var pointOnCircle1 = FindPointOnCircle(middleRadius, point => new Point(point.X + 1, point.Y + 1));
            var pointOnCircle2 = FindPointOnCircle(middleRadius, point => new Point(point.X - 1, point.Y + 1));
            var diagonalRadius1 = GetClosestLocation(pointOnCircle1, corners).DistanceTo(center);
            var diagonalRadius2 = GetClosestLocation(pointOnCircle2, corners).DistanceTo(center);
            var radius = (radiuses.Sum() + diagonalRadius1 + diagonalRadius2) / 6;
            var startRange = 80 * (int)radius / 100;
            var endRange = 120 * (int)radius / 100;

            radiuses.Select(r => r.Should().BeInRange(startRange, endRange));
            diagonalRadius1.Should().BeInRange(startRange, endRange);
            diagonalRadius2.Should().BeInRange(startRange, endRange);
        }

        private Point FindPointOnCircle(double radius, Func<Point, Point> step)
        {
            Point point = center;
            double distance = 0;
            while (radius >= distance)
            {
                point = step(point);
                distance = point.DistanceTo(center);
            }
            return point;
        }

        private Point GetClosestLocation(Point diagonalPoint, HashSet<Point> points)
        {
            var min = double.MaxValue;
            var closestPoint = new Point();
            foreach (var point in points)
                if (diagonalPoint.DistanceTo(point) < min)
                {
                    min = diagonalPoint.DistanceTo(point);
                    closestPoint = point;
                }
            return closestPoint;
        }

        [TestCase(100, 1, 50, TestName = "OnSmallRectangles")]
        [TestCase(100, 200, 1000, TestName = "OnHugeRectangles")]
        public void PutNextRectangle_WithoutIntersectionWithAdded(int count, int min, int max)
        {
            var random = new Random();
            rectangles = Enumerable.Range(0, count).Select(b => circularCloudLayouter.PutNextRectangle(new Size(random.Next(min, max), random.Next(min, max)))).ToList();

            rectangles
                .ForEach(rec1 => rectangles
                    .Where(rec2 => rec2 != rec1)
                    .ToList()
                    .ForEach(rec2 => rec2.IntersectsWith(rec1).Should().BeFalse()));
        }

        [Test]
        public void ReturnPositionClosestToCenter_When_CanNotBeConnectedWithCenter()
        {
            sizesForTesting
                .Select(size => circularCloudLayouter.PutNextRectangle(size).Location)
                .Should()
                .BeEquivalentTo(expectedLocations);
        }


        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var pathToTestNameDir = Path.Combine(TestContext.CurrentContext.TestDirectory,  TestContext.CurrentContext.Test.Name);
                var pathToCurrentTestNumberDir = GetPathToDirectoryForSaving(pathToTestNameDir);
                SaveFiles(pathToCurrentTestNumberDir);
                Console.WriteLine($"Tag cloud visualization saved to file {pathToCurrentTestNumberDir}");
            }
            rectangles.Clear();
        }

        private string GetPathToDirectoryForSaving(string pathToTestNameDir)
        {
            CreateDirectory(pathToTestNameDir);
            var dirsCount = Directory.GetDirectories(pathToTestNameDir).Length.ToString();
            var pathToCurrentTestNumberDir = Path.Combine(Path.Combine(pathToTestNameDir, dirsCount));
            CreateDirectory(pathToCurrentTestNumberDir);
            return pathToCurrentTestNumberDir;
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private void SaveFiles(string path)
        {
            var pathToFile = Path.Combine(path, "Rectangles");
            var imageSettings = new ImageSettings(new Size(1800, 1800), pathToFile, ".png", 0);
            var debugVisualization = new DebugVisualization(imageSettings, rectangles);
            debugVisualization.Draw();
            File.WriteAllText(Path.Combine(path, "Rectangles.json"), JsonConvert.SerializeObject(rectangles));
        }
    }
}

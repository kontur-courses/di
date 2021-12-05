using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Creators;
using TagCloud.Layouters;

namespace TagCloudTests
{
    public class CircularCloudLayouterTests
    {
        private WrappedCloudLayouter layouter;
        private Point center;

        [SetUp]
        public void SetUp()
        {
            center = new Point(100, 100);
            layouter = new WrappedCloudLayouter(center);
        }

        [TestCase(0, 0, TestName = "With Zero Point")]
        [TestCase(100, 100, TestName = "With Non Zero Point")]
        public void CtorShouldSetCenter(int x, int y)
        {
            var layouter = new WrappedCloudLayouter(new Point(x, y));
            var expected = new Point(x, y);

            layouter.Center.Should().Be(expected);
        }

        [TestCaseSource(nameof(CasesForPutNextRectangles))]
        public void PutNextRectangles(int rectanglesNumber, Point expectedLocation)
        {
            var size = new Size(50, 50);
            var tags = new Tag[rectanglesNumber + 1];

            for (var i = 0; i < rectanglesNumber + 1; i++)
                tags[i] = new Tag("", 0, size);
            var puttedTags = layouter.PutTags(tags);

            puttedTags.First(t => t.ContainingRectangle.Location == expectedLocation)
                .ContainingRectangle
                .Location
                .Should().Be(expectedLocation);
        }

        private static IEnumerable<TestCaseData> CasesForPutNextRectangles
        {
            get
            {
                yield return new TestCaseData(0, new Point(75, 75))
                    .SetName("First Rectangle Should Be Center");
                yield return new TestCaseData(1, new Point(75, 25))
                    .SetName("Second Rectangle Should Be Over First");
                yield return new TestCaseData(4, new Point(25, 75))
                    .SetName("Rectangle Should Go Clockwise");
            }
        }

        [TestCaseSource(nameof(CasesForPutNextRectangleThrows))]
        public void PutNextRectangle_ShouldThrows(Tag tag)
        {
            var tags = new Tag[1];
            tags[0] = tag;

            Assert.Throws<ArgumentException>(() => layouter.PutTags(tags).First());
        }

        private static IEnumerable<TestCaseData> CasesForPutNextRectangleThrows
        {
            get
            {
                yield return new TestCaseData(new Tag("", 0, new Size(0, 1)))
                    .SetName("When Size Width Zero");
                yield return new TestCaseData(new Tag("", 0, new Size(1, 0)))
                    .SetName("When Size Height Zero");
                yield return new TestCaseData(new Tag("", 0, new Size(0, 0)))
                    .SetName("When Size Width And Height Zero");
                yield return new TestCaseData(new Tag("", 0, new Size(-1, 1)))
                    .SetName("When Negative Size Width");
                yield return new TestCaseData(new Tag("", 0, new Size(1, -1)))
                    .SetName("When Negative Size Height");
                yield return new TestCaseData(new Tag("", 0, new Size(-1, -1)))
                    .SetName("When Negative Size Width And Height");
            }
        }

        [Test]
        public void PutNextRectangle_ShouldNotIntersectWithOther()
        {
            var tags = new List<Tag>();
            var size = new Size(10, 10);

            for (var i = 0; i < 50; i++)
                tags.Add(new Tag("", 0, size));
            var rectanglesCopy = layouter.PutTags(tags).ToArray();

            tags.Count(rect => rectanglesCopy
                .All(r => r.ContainingRectangle.IntersectsWith(rect.ContainingRectangle)))
                .Should().Be(0);
        }

        [Test]
        public void PutNextRectangle_ShouldBeDense()
        {
            var tags = new Tag[100];
            var random = new Random();

            for (var i = 0; i < 100; i++)
            {
                tags[i] = new Tag("", 0, new Size(random.Next(5, 100), random.Next(5, 100)));

            }
            
            var puttedTags = layouter.PutTags(tags);
            CalculateDensityRatio(puttedTags.Select(t => t.ContainingRectangle)).Should().BeGreaterOrEqualTo(0.5);
        }

        private double CalculateDensityRatio(IEnumerable<Rectangle> rectangles)
        {
            var circleRadius = GetCircumscribeCircleRadius(rectangles);
            var rectanglesSquare = rectangles.Sum(r => r.Width * r.Height);
            var circleSquare = Math.PI * circleRadius * circleRadius;
            return rectanglesSquare / circleSquare;
        }

        private double GetCircumscribeCircleRadius(IEnumerable<Rectangle> rectangles)
        {
            return rectangles.SelectMany(GetRectangleCorners)
                .Max(current => center.DistanceTo(current));
        }

        private IEnumerable<Point> GetRectangleCorners(Rectangle rectangle)
        {
            yield return new Point(rectangle.Left, rectangle.Top);
            yield return new Point(rectangle.Left, rectangle.Bottom);
            yield return new Point(rectangle.Right, rectangle.Top);
            yield return new Point(rectangle.Right, rectangle.Bottom);
        }
    }

    internal class WrappedCloudLayouter
    {
        internal Point Center { get; }
        private readonly ICloudLayouter layouter;

        internal WrappedCloudLayouter(Point center)
        {
            Center = center;
            layouter = new CircularCloudLayouter(center);
        }

        internal IEnumerable<Tag> PutTags(IEnumerable<Tag> tags) 
            => layouter.PutTags(tags);
        
    }
}
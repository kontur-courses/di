using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Specialized;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagCloudVisualization
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        [SetUp]
        public void SetUp()
        {
            center = new Point(3, 4);
            layouter = new CircularCloudLayouter(center, new RoundSpiralGenerator());
        }

        [TearDown]
        public void TearDown()
        {
            if (!TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Failed))
                return;
            var path = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory));
            var name = $@"{TestContext.CurrentContext.Test.Name}.png";
            path = Path.Combine(path, name);
            layouter.Rectangles.DrawRectangles(layouter.Center, path);
        }

        private CircularCloudLayouter layouter;
        private Point center;

        [TestCase(0, 1, TestName = "has zero as width")]
        [TestCase(1, 0, TestName = "has zero as height")]
        [TestCase(-1, 5, TestName = "has negative width")]
        [TestCase(1, -5, TestName = "has negative height")]
        public void ThrowArgumentException_WhenSize(int w, int h)
        {
            Action addition = () => layouter.PutNextRectangle(new Size(w, h));
            addition.Should()
                    .Throw<ArgumentException>()
                    .WithMessage("size has non positive parts");
        }

        [TestCase(100, 1000, TestName = "N = 100, M = 10 ms")]
        [TestCase(100, 1000, TestName = "N = 10000, M = 1000 ms")]
        public void AddNRectangles_FasterThanMms(int n, int m)
        {
            void Addition()
            {
                var random = new Random(42);

                for (var i = 0; i < n; i++)
                    layouter.PutNextRectangle(new Size(random.Next(10, 50), random.Next(10, 30)));
            }

            new ExecutionTime((Action) Addition).Should()
                                                .BeLessThan(new TimeSpan(0, 0, 0, 0, m));
            layouter.Rectangles.Should()
                    .HaveCount(n);
        }

        public static IEnumerable<Size> GenerateRectangles(SizeSequenceCreators.CreatorInfo creatorInfo) =>
            Enumerable.Range(creatorInfo.Start, creatorInfo.Count)
                      .Select(creatorInfo.Creator);

        [Test]
        public void AddRectangleToRectangles()
        {
            for (var i = 1; i < 6; i++) layouter.PutNextRectangle(new Size(i, 3 * i));

            layouter.Rectangles.Should()
                    .NotContainNulls()
                    .And.HaveCount(5);
        }

        [Test]
        public void AddSeveralRectanglesToRectangles()
        {
            var rectangles = layouter.PutNextRectangles(Enumerable.Range(10, 10)
                                                                  .Select((n, i) => new Size(n, i + 1)))
                                     .ToList();
            rectangles.Should()
                      .NotContainNulls()
                      .And.HaveCount(10);
        }

        [Test]
        public void HaveDenseWordCloud_WhenManyRectanglesWasAdded()
        {
            var rectangles = layouter.PutNextRectangles(GenerateRectangles(SizeSequenceCreators.SlowDecreasing))
                                     .ToList();
            var summaryArea = rectangles.Sum(r => r.Area());
            var cloudSize = rectangles.GetSize();
            var radius = Math.Min(cloudSize.Width, cloudSize.Height);
            var circleArea = Math.PI * radius * radius;

            summaryArea.Should()
                       .BeLessOrEqualTo((int) circleArea);
        }

        [Test]
        public void HaveZeroIntersections_WhenManyRectanglesWasAdded()
        {
            var rectangles = layouter.PutNextRectangles(GenerateRectangles(SizeSequenceCreators.SlowDecreasing))
                                     .ToList();

            rectangles.Aggregate(Rectangle.Intersect)
                      .IsEmpty.Should()
                      .BeTrue();
        }

        [Test]
        public void PutFirstRectangleToCenter()
        {
            var size = new Size(3, 4);
            layouter.PutNextRectangle(size)
                    .Should()
                    .BeEquivalentTo(new Rectangle(center, size));
        }

        [Test]
        public void PutSecondRectangle_SoThatItDoesNotIntersectsWithFirst()
        {
            var first = layouter.PutNextRectangle(new Size(3, 4));
            var second = layouter.PutNextRectangle(new Size(5, 6));
            second.IntersectsWith(first)
                  .Should()
                  .BeFalse();
        }
    }
}

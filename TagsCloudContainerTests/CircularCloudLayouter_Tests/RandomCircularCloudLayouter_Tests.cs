using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.CircularCloudLayouters;
using TagsCloudContainer.Settings;
using TagsCloudContainerTests.Extensions;

namespace TagsCloudContainerTests.CircularCloudLayouter_Tests
{


    [TestFixture]
    public class RandomCircularCloudLayouter_Tests
    {
        private RandomCircularCloudLayouter cloudLayouter;
        private Point centerPoint;
        private readonly Random random = new Random();
        private RandomAngleChooser randomAngleChooser;

        [SetUp]
        public void SetUp()
        {
            var imageSettings = new ImageSettings();
            var size = imageSettings.ImageSize;
            centerPoint = new Point(size.Width / 2, size.Height / 2);
            randomAngleChooser = new RandomAngleChooser(random);
            cloudLayouter = new RandomCircularCloudLayouter(new ImageSettings(), randomAngleChooser);
        }

        [Test]
        public void PutNextRectangle_ContainsCenter_AtFirstExecution()
        {
            var size = new Size(100, 100);
            cloudLayouter.PutNextRectangle(size).Contains(centerPoint).Should().BeTrue();
        }

        [Test]
        public void PutNextRectangle_ReturnsNonIntersectedRectangles()
        {
            var sizes = CreateSizes(100);

            var rectangles = cloudLayouter.PutNextRectangles(sizes);

            AssertThatRectanglesDoNotIntersect(rectangles);
        }

        [Test]
        public void PutNextRectangles_ReturnsDifferentRectangles_OnSameLayouterWithSameSizes_AtDifferentExecution()
        {
            var sizes = CreateSizes(50).ToList();
            var secondCloudLayouter = new RandomCircularCloudLayouter(new ImageSettings(), randomAngleChooser);

            var firstRectangles = cloudLayouter.PutNextRectangles(sizes).ToList();
            var secondRectangles = secondCloudLayouter.PutNextRectangles(sizes).ToList();


            firstRectangles.Should().NotBeEquivalentTo(secondRectangles);
        }

        [Test]
        public void PutNextRectangles_ReturnsRectangles_WithSameSizes()
        {
            var sizes = CreateSizes(50).ToList();

            var rectangles = cloudLayouter.PutNextRectangles(sizes);

            rectangles.Select(x => x.Size).ShouldBeEquivalentTo(sizes, a => a.WithStrictOrdering());
        }

        [Test, Timeout(1000)]
        public void PutNextRectangles_WorksFast_WhenThereAreManyRectangles()
        {
            var sizes = CreateSizes(1000).ToList();

            var rectangles = cloudLayouter.PutNextRectangles(sizes);
        }

        private void AssertThatRectanglesDoNotIntersect(List<Rectangle> rectangles)
        {
            rectangles.SelectMany(x => rectangles.Select(y => Tuple.Create(x, y)))
                .Where(x => x.Item1 != x.Item2)
                .Select(x => x.Item1.IntersectsWith(x.Item2))
                .ShouldAllBeEquivalentTo(false);
        }

        private IEnumerable<Size> CreateSizes(int count)
            => Enumerable.Range(0, count)
                .Select(x => x % 100)
                .Select(x => new Size(x + 20, x + 20));

    }
}
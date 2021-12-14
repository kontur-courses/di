using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloudContainerTests.CircularCloudLayouterTests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
#pragma warning disable CS8618
        private TagsCloudContainerCore.CircularLayouter.CircularCloudLayouter cloudLayouter;
#pragma warning restore CS8618

        
        [SetUp]
        public void SetCloudLayouter()
        {
            cloudLayouter = new TagsCloudContainerCore.CircularLayouter.CircularCloudLayouter(new Point(0, 0));
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        [TestCase(-1, -1)]
        public void Constructor_AnyPoint_ShouldNotChangeParameters(int x, int y)
        {
            var location = new Point(x, y);

            cloudLayouter = new TagsCloudContainerCore.CircularLayouter.CircularCloudLayouter(location);

            cloudLayouter.Center.Should().Be(location,
                "Constructor shouldn't change central point");
        }

        
        [TestCase(0, 0, 2, 2, -1, -1)]
        [TestCase(0, 0, 3, 3, -2, -2)]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(3, 4, 5, 7, 0, 0)]
        public void PutNextRectangle_FirstRectangle_ShouldPutOnCenter
        (int cX, int cY, int width, int height, int expX,
            int expY)
        {
            var center = new Point(cX, cY);
            cloudLayouter = new TagsCloudContainerCore.CircularLayouter.CircularCloudLayouter(center);
            var size = new Size(width, height);

            var expectedLocation =
                new Point(expX, expY);

            var actualRectangle = cloudLayouter.PutNextRectangle(size);

            actualRectangle.Location.Should().Be(expectedLocation,
                "The first rectangle should be in the Center");
        }


        [Test]
        public void PutNextRectangle_NextRectangles_ShouldNotHaveIntersectionsSimpleCase()
        {
            var size = new Size(3, 2);

            PutManyRectangles(size, 20);
            
            Assert.IsFalse(HasIntersectingRectangles(cloudLayouter.GetRectangles()));
        }


        [Test]
        public void PutNextRectangle_NextRectangles_ShouldNotHaveIntersectionsDifferentSizes()
        {
            var sizes = new[]
            {
                new Size(1, 2),
                new Size(40, 60),
                new Size(85, 50),
                new Size(100, 100),
                new Size(300, 450),
                new Size(219, 168),
                new Size(37, 27),
                new Size(1, 2),
                new Size(40, 60),
            };


            PutManiRectangles(sizes);
            Assert.IsFalse(HasIntersectingRectangles(cloudLayouter.GetRectangles()));
        }


        private void PutManyRectangles(Size size, int count)
        {
            for (; count > 0; count--)
                cloudLayouter.PutNextRectangle(size);
        }


        private bool HasIntersectingRectangles(IEnumerable<Rectangle> rectangles)
            => rectangles
                .SelectMany(x => cloudLayouter.GetRectangles()
                    .Select(y => (x, y))).Where(x => x.x != x.y)
                .Any(pair => pair.x.IntersectsWith(pair.y));

        
        private void PutManiRectangles(IEnumerable<Size> sizes)
        {
            foreach (var size in sizes)
                cloudLayouter.PutNextRectangle(size);
        }
    }
}
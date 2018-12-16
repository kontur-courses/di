using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using WordCloud.LayoutGeneration.Layoter;
using WordCloud.LayoutGeneration.Layoter.Circular;

namespace TagCloudTests
{
   [TestFixture]
    class CircularCloudLayouter_Tests
    {
        private CircularCloudLayouter layouter;
        
        [SetUp]
        public void SetUp()
        {
            var center = new Point(100,100);
            layouter = new CircularCloudLayouter(center);
        }

        [Test]
        public void LayouterIsEmpty_AfterCreation()
        {
            layouter.rectangleCloud.Rectangles.Should().BeEmpty();
        }

        [Test]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        [TestCase(-1, -1)]
        public void Constructor_ThrowsArgumentException_OnNegativeCoordinates(int x, int y)
        {
            var center = new Point(x, y);
            Action act = () => new CircularCloudLayouter(center);;
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutNextRectangle_SetrectangleToCenter_OnFirstTime()
        {
            var rectangle = layouter.PutNextRectangle(new Size(50, 40));
            var expectedCenter = new Point(100, 100);
            var firstRectangleCenter = rectangle.Location + new Size(rectangle.Width / 2, rectangle.Height / 2);
            firstRectangleCenter.Should().Be(expectedCenter);
        }

        [Test]
        public void GetRectangleCenter_EqualsLayouterCenter_FirstTime()
        {
            var rectangle = layouter.PutNextRectangle(new Size(25, 25));
            var expectedCenter = new Point(100, 100);

            var actual = rectangle.GetRectangleCenter();

            actual.Should().BeEquivalentTo(expectedCenter);;
        }

        [TestCase(0, 0, 100, 50, ExpectedResult = true, TestName = "Intersecting rectangles")]
        [TestCase(130, 130, 10, 40, ExpectedResult = false, TestName = "Non-ntersecting rectangles")]
        public bool IntersectsWithOtherRectangles(int x, int y, int width, int height)
        {
            var rectangle = new Rectangle(x, y, width, height);
            var rectangles = new[] { new Rectangle(0, 0, 10, 10), new Rectangle(20, 20, 100, 50) };
            return rectangle.IntersectsWithRectangles(rectangles);
        }

        [Test]
        public void Reset_TagCloudEmpty()
        {
            layouter.PutNextRectangle(new Size(25, 25));
            layouter.PutNextRectangle(new Size(25, 25));
            layouter.PutNextRectangle(new Size(25, 25));

            layouter.Reset();
            layouter.rectangleCloud.Rectangles.Should().BeEmpty();

        }

        [TearDown]
        public void CreateImade_IfTestFails()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                //var path = Path.Combine(TestContext.CurrentContext.TestDirectory, TestContext.CurrentContext.Test.Name + ".bmp");
                //var vizualizer = new Vizualizer();
                //var layout = vizualizer.GetLayoutImage(layouter.TagsCloud.Rectangles);
                //vizualizer.SaveImage(path, layout);
                //Console.WriteLine($"Tag cloud visualization saved to file {path}");
            }
        }
    }
}
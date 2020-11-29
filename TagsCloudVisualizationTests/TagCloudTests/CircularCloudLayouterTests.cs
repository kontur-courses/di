using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization.PointsGenerators;
using TagsCloudVisualization.TagCloud;

namespace TagsCloudVisualizationTests.TagCloudTests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        [SetUp]
        public void SetUp()
        {
            var centerPoint = new Point(250, 250);
            pointGenerator = new ArchimedesSpiral(centerPoint);
            sut = new CircularCloudLayouter(pointGenerator);
            addedRectangles = new List<Rectangle>();
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.FailCount == 0)
                return;
            
            var pathToDirectory = @"..\..\..\FailedTests\";
            var fileName = TestContext.CurrentContext.Test.Name;
            TagCloudVisualizer.PrintTagCloud(addedRectangles, sut.Center, 100, 100, 
                pathToDirectory, fileName);
            
            Console.WriteLine($"Tag cloud visualization saved to file {pathToDirectory}{fileName}");
        }
        
        private ICloudLayouter sut;
        private IPointGenerator pointGenerator;
        private List<Rectangle> addedRectangles;

        [Test]
        public void InitializeCircularCloudLayouter_Throw_NullPointGenerator()
        {
            Assert.Throws<ArgumentException>(
                () => new CircularCloudLayouter(null));
        }

        [Test]
        public void GetAddedRectangle_RectCentralPointInCenter_WhenPutOneRectangle()
        {
            var size = new Size(200, 200);
            addedRectangles.Add(sut.PutNextRectangle(size));

            var firstAddedRect = addedRectangles.First();

            firstAddedRect.Location.Should().Be(new Point(sut.Center.X - firstAddedRect.Width / 2,
                sut.Center.Y - firstAddedRect.Height / 2));
        }

        [TestCase(0)]
        [TestCase(5)]
        public void PutNextRectangle_CorrectRectanglesCount_ReturnPutRectangles(int expectedCount)
        {
            for (int i = 0; i < expectedCount; i++)
                addedRectangles.Add(sut.PutNextRectangle(new Size(200, 200)));

            addedRectangles.Count().Should().Be(expectedCount);
        }

        [Test]
        public void GetAddedRectangle_ReturnAddedRectangle_WhenPutOneRectangle()
        {
            var size = new Size(200, 200);
            var expectedRects = new List<Rectangle>
            {
                new Rectangle(
                    new Point(sut.Center.X - size.Width / 2,
                        sut.Center.Y - size.Height / 2), size)
            };
            
            addedRectangles.Add(sut.PutNextRectangle(size));
            
            addedRectangles.Should().Equal(expectedRects);
        }

        [TestCase(100, -100)]
        [TestCase(-100, 100)]
        [TestCase(100, 0)]
        [TestCase(0, 100)]
        public void PutNextRectangle_Throws_IncorrectArguments(int width, int height)
        {
            Assert.Throws<ArgumentException>(() => sut.PutNextRectangle(new Size(width, height)));
        }

        [Test]
        public void PutNextRectangle_ReturnsCorrectSecondRectPosition_WhenPutTwoRectangles()
        {
            addedRectangles.Add(sut.PutNextRectangle(new Size(50, 50)));
            addedRectangles.Add(sut.PutNextRectangle(new Size(50, 50)));
            var expectedPosition = new Point(275, 219);

            var secondAddedRectangle = addedRectangles.Skip(1).First();

            secondAddedRectangle.Location.Should().Be(expectedPosition);
        }

        [Test]
        public void PutNextRectangle_PointsGenerationStartsOver_WhenPutOneRectangle()
        {
            sut.PutNextRectangle(new Size(300, 300));

            pointGenerator.GetNextPoint().Should().Be(sut.Center);
        }
        
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        public void GetAddedRectangles_RectanglesShouldNotIntersect_WhenPutManyRectangles(int addedRectanglesCount)
        {
            for (int i = 0; i < addedRectanglesCount; i++)
                addedRectangles.Add(sut.PutNextRectangle(new Size(50, 50)));

            foreach (var addedRectangle in addedRectangles)
                addedRectangles.All(
                    rectangle => rectangle.IntersectsWith(addedRectangle)).Should().Be(false);
        }
    }
}
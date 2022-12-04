using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private List<Rectangle> rectangles;
        private Point center;
        private CircularCloudLayouter sut;

        [OneTimeSetUp]
        public void StartTests()
        {
            center = new Point(200, 100);
        }
        
        [SetUp]
        public void SetupTest()
        {
            rectangles = new List<Rectangle>();
            sut = new CircularCloudLayouter(center);
        }
        
        [Test]
        public void PutNextRectangle_FirstGotRectangle_ShouldContainsCenter()
        {
            rectangles.Add(sut.PutNextRectangle(new Size(10, 5)));
            rectangles.First().Contains(center).Should().Be(true);
        }
        
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(40)]
        [TestCase(100)]
        public void PutNextRectangle_ShouldReturnRectangles_WithoutIntersections(int count)
        {
            var size = new Size(20, 5);
            rectangles = Enumerable.Range(0, count)
                .Select(item => sut.PutNextRectangle(size))
                .ToList();
            for (var i = 0; i < count; i++)
            {
                var rect = rectangles[i];
                for (var j = 0; j < count; j++)
                {
                    if (i != j)
                        rect.IntersectsWith(rectangles[j]).Should().Be(false);
                }
            }
        }

        [TearDown]
        public void SaveImage_OnFailTest()
        {
            var a = TestContext.CurrentContext;
            if (a.Result.FailCount == 0)
                return;
            var filename = $"Failed test {TestContext.CurrentContext.Test.Name} image at {DateTime.Now:dd-MM-yyyy HH_mm_ss}.jpg";
            var bitmap = TagCloudDrawer.DrawWithAutoSize(rectangles.ToArray(),
                Color.Black, Color.DarkOrange,
                true, true);
            bitmap.Save(filename);
            var path = AppContext.BaseDirectory;
            TestContext.Error.Write($"Tag cloud visualization saved to file {path + filename}");
        }
    }
}
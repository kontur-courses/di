using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualisation.App.RectanglesLayouters.SpiralCloudLayouters;
using TagsCloudVisualisation.Tests.CloudLayouter.SpiralCloudLayouter.Infrastructure;

namespace TagsCloudVisualisation.Tests.CloudLayouter.SpiralCloudLayouter
{
    [TestFixture]
    public class CircularCloudLayouterTest
    {
        private List<Rectangle>? rectangles;
        private Point center;
        private App.RectanglesLayouters.SpiralCloudLayouters.SpiralCloudLayouter? sut;
        private SpiralCloudLayouterSettings? settings;

        [OneTimeSetUp]
        public void StartTests()
        {
            center = new Point(200, 100);
            settings = new SpiralCloudLayouterSettings()
            {
                Center = center,
                RotationStep = 0.1,
                SpiralStep = 0.1
            };
            sut = new App.RectanglesLayouters.SpiralCloudLayouters.SpiralCloudLayouter();
            sut.SetSettings(settings);
        }
        
        [SetUp]
        public void SetupTest()
        {
            rectangles = new List<Rectangle>();
        }
        
        [Test]
        public void PutNextRectangle_FirstGotRectangle_ShouldContainsCenter()
        {
            rectangles!.Add(sut!.PutNextRectangle(new Size(10, 5)));
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
                .Select(item => sut!.PutNextRectangle(size))
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
            var bitmap = TagCloudDrawer.DrawWithAutoSize(rectangles!.ToArray(),
                Color.Black, Color.DarkOrange,
                true, true);
            bitmap.Save(filename);
            var path = AppContext.BaseDirectory;
            TestContext.Error.Write($"Tag cloud visualization saved to file {path + filename}");
        }
    }
}
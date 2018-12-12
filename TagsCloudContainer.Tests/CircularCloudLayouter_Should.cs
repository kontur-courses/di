using System;
using System.Drawing;
using System.IO;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudContainer.Algorithms;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    class CircularCloudLayouter_Should
    {
        private CircularCloudAlgorithm layouter;
        private int centerWidth = 1000;
        private int centerHeight = 1000;
        Point center;

        [SetUp]
        public void SetUp()
        {
            var settings = A.Fake<ICloudSettings>();

            center = new Point(centerWidth, centerHeight);
            layouter = new CircularCloudAlgorithm(settings, new ArchimedeanSpiral(settings));
        }
        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var path = TestContext.CurrentContext.TestDirectory;
                var fileName = $"{TestContext.CurrentContext.Test.Name} failed.png";
                var outputFileName = Path.Combine(path, fileName);

                var size = new Size(2 * centerWidth, 2 * centerHeight);
                using (var bitmap = new Bitmap(size.Width, size.Height))
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        foreach (var rectangle in layouter.GetRectangles())
                        {
                            graphics.DrawRectangle(new Pen(Color.Red), rectangle);
                        }
                        bitmap.Save(outputFileName);
                    }
                }

                TestContext.WriteLine($"Tag cloud visualization saved to file {outputFileName}");
            }
        }

        [Test]
        public void HaveEmptyRectanglesCollection_OnInitialization()
        {
            layouter.GetRectangles().Should().BeEmpty();
        }

        [Test]
        public void ThrowArgumentException_OnPuttingNextRectangleOfEmptySize()
        {
            Action action = () => layouter.PutNextRectangle(new Size());
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ThrowArgumentException_OnPuttingNextRectangleOfNegativeSize()
        {
            Action action = () => layouter.PutNextRectangle(new Size(-5, -5));
            action.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void AddSeveralRectangles()
        {
            for (int i = 0; i < 1000; i++)
            {
                layouter.PutNextRectangle(new Size(100, 50));
            }
            layouter.GetRectangles().Count.Should().Be(1000);
        }

        [Test]
        public void AddRectanglesWithoutIntersections()
        {
            for (int i = 0; i < 1000; i++)
            {
                layouter.PutNextRectangle(new Size(100, 50));
            }

            foreach (var rectangle in layouter.GetRectangles())
            {
                layouter.GetRectangles()
                    .All(e => e.IntersectsWith(rectangle)).Should().BeFalse();
            }
        }
    }
}

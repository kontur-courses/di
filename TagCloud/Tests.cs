using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;


namespace TagsCloudVisualization
{
    class Tests
    {
        private CircularCloudLayouter cloud;
        private TestContext currentContext;
        private const string filePath = "D://DebugFile.bmp";

        [SetUp]
        public void SetUp()
        {
            cloud = new CircularCloudLayouter(new Point(0, 0), 0.5);
            currentContext = TestContext.CurrentContext;
        }

        [TearDown]
        public void TearDown()
        {
            if (currentContext.Result.FailCount != 0)
            {
                var visualisator = new Visualiser(new Size(600, 600), filePath);
                visualisator.RenderCurrentConfig(cloud);
                Console.WriteLine("Tag cloud visualization saved to file " + filePath);
            }
        }

        [Test]
        public void TagCloudConstructor_ShouldMakeCenter()
        {
            cloud.Center.Should().Be(new Point(0, 0));
        }


        [Test]
        public void FirstRectange_ShouldBeNearCenter()
        {
            var rect = cloud.PutNextRectangle(new Size(10, 10));
            rect.Contains(cloud.Center).Should().BeTrue();
        }

        [Test]
        public void Rectangles_ShouldNotIntersect()
        {
            var firstRectangle = cloud.PutNextRectangle(new Size(20, 10));
            var secondRectangle = cloud.PutNextRectangle(new Size(10, 20));
            firstRectangle.IntersectsWith(secondRectangle).Should().BeFalse();
        }

        [TestCase(-1, 1, TestName = "PutNextRectangle Should Throw Argument Exception With Negative Width")]
        [TestCase(0, 1, TestName = "PutNextRectangle Should Throw Argument Exception With Zero Width")]
        [TestCase(1, -1, TestName = "PutNextRectangle Should Throw Argument Exception With Negative Height")]
        [TestCase(1, 0, TestName = "PutNextRectangle Should Throw Argument Exception With Zero Height")]
        public void test(int width, int height)
        {
            var size = new Size(width, height);
            Assert.Throws<ArgumentException>(() => cloud.PutNextRectangle(size));
        }
        [Test]
        public void FirstRectangle_ShouldBeInCenter()
        {
            var rect = cloud.PutNextRectangle(new Size(10, 10));
            rect.Location.Should().Be(new Point(0, 0));
        }
        [Test]
        public void ArchimedesSpiralePointsMaker_ShouldReturnCenterAtFirst()
        {
            var value = ArchimedesSpiralePointsMaker
                .GenerateNextPoint(new Point(0, 0), 2).First();
            value.Should().Be(new Point(0, 0));
        }
    }
}

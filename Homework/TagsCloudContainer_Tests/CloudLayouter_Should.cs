using System;
using System.Drawing;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.Layouter.PointsCreators;

namespace TagsCloudContainer_Tests
{
    [TestFixture]
    public class CloudLayouter_Should
    {
        private readonly Size testingSize = new Size(10,10);
        private readonly Point testingCenter = new Point(5,5);

        private CloudLayouter sut;
        
        [SetUp]
        public void SetUp()
        {
            var pointsProvider = A.Fake<IPointsProvider>();
            A.CallTo(() => pointsProvider.GetNextPoint())
                .Returns(testingCenter)
                .Once();
            sut = new CloudLayouter(pointsProvider);
        }

        [TestCase(0, 3, TestName = "width of rectangle is not expected to be zero")]
        [TestCase(3, 0, TestName = "height of rectangle is not expected to be zero")]
        [TestCase(-1, 3, TestName = "width of rectangle is not expected to be negative")]
        [TestCase(3, -1, TestName = "height of rectangle is not expected to be zero")]
        public void Throw_WhenIncorrectSize(int width, int height)
        {
            var size = new Size(width, height);
            Assert.Throws<ArgumentException>(() => sut.PutNextRectangle(size));
        }
        
        [Test]
        public void PutFirstRectangleInCenter()
        {
            sut.PutNextRectangle(testingSize).Should().Be(new Rectangle(testingCenter, testingSize));
        }
    }
}
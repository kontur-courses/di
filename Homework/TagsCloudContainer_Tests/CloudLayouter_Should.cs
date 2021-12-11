using System;
using System.Drawing;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.Layouter.PointsProviders;

namespace TagsCloudContainer_Tests
{
    [TestFixture]
    public class CloudLayouter_Should
    {
        [SetUp]
        public void SetUp()
        {
            pointsProvider = A.Fake<IPointsProvider>();
            A.CallTo(() => pointsProvider.GetNextPoint())
                .Returns(testingCenter)
                .Once();
            sut = new CloudLayouter(pointsProvider);
        }

        private readonly Size testingSize = new(10, 10);
        private readonly Point testingCenter = new(5, 5);

        private CloudLayouter sut;
        private IPointsProvider pointsProvider;

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

        [Test]
        public void AskAnotherPoint_WhenRectangleIntersectsWithOthers()
        {
            A.CallTo(() => pointsProvider.GetNextPoint())
                .ReturnsNextFromSequence(testingCenter, testingCenter + new Size(1, 1), testingCenter + testingSize);
            sut.PutNextRectangle(testingSize);
            sut.PutNextRectangle(testingSize);
            A.CallTo(() => pointsProvider.GetNextPoint())
                .MustHaveHappenedANumberOfTimesMatching(i => i == 3);
        }
    }
}
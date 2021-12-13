using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.Layouter.PointsProviders;

namespace TagsCloudContainer_Tests
{
    [TestFixture]
    public class SpiralPointsProvider_Should
    {
        private readonly ITagCloudSettings settings = A.Fake<ITagCloudSettings>();

        [Test]
        public void CreateFirstPointInCenter()
        {
            A.CallTo(() => settings.ImageWidth).Returns(1000);
            A.CallTo(() => settings.ImageHeight).Returns(1000);
            var creator = new SpiralPointsProvider(settings);
            var expected = new Point(settings.ImageWidth / 2, settings.ImageHeight / 2);
            creator.GetNextPoint().Should().Be(expected);
        }

        [Test]
        public void CreateDifferentPoints()
        {
            var pointsCount = 100;
            var creator = new SpiralPointsProvider(settings);
            var points = new List<Point>();
            for (var i = 1; i <= pointsCount; i++) points.Add(creator.GetNextPoint());
            points.Should().HaveSameCount(points.Distinct());
        }

        [Test]
        public void CreateExpectedNumberOfPoints()
        {
            var pointsCount = 100;
            var creator = new SpiralPointsProvider(settings);
            var points = new List<Point>();
            for (var i = 1; i <= pointsCount; i++) points.Add(creator.GetNextPoint());

            points.Should().HaveCount(pointsCount);
        }
    }
}
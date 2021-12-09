using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests.Tests
{
    public class ArchimedeanSpiralPathTests
    {
        private ArchimedeanSpiralPath defaultPath;

        [SetUp]
        public void SetUp()
        {
            defaultPath = new ArchimedeanSpiralPath(new ArchimedeanSpiral());
        }

        [Test]
        public void Constructor_ThrowException_WithNegativeStep()
        {
            Assert.Throws<ArgumentException>(
                () =>
                    new ArchimedeanSpiralPath(new ArchimedeanSpiral(), -1));
        }

        [Test]
        public void Constructor_ThrowException_WithNullSpiral()
        {
            Assert.Throws<ArgumentException>(
                () =>
                    new ArchimedeanSpiralPath(null));
        }

        [Test]
        public void GetEnumerator_ReturnsOnlyUniquePoints()
        {
            var points = defaultPath.Take(100).ToList();
            points.Should().OnlyHaveUniqueItems();
        }

        [Test]
        public void GetEnumerator_ReturnsSamePoints_AfterReset()
        {
            var firstPoints = defaultPath.Take(100).ToList();
            defaultPath.Reset();

            var secondPoints = defaultPath.Take(100).ToList();

            secondPoints.Should().BeEquivalentTo(firstPoints);
        }

        [Test]
        public void GetEnumerator_SavesDegree_AfterEnumerating()
        {
            defaultPath.First();
            var initialDegree = defaultPath.Degree;

            defaultPath.First();

            defaultPath.Degree.Should().BeGreaterThan(initialDegree);
        }

        [Test]
        public void Reset_SetDegreeToZero_AfterEnumerating()
        {
            defaultPath.GetNextPoint();
            defaultPath.Reset();
            defaultPath.Degree.Should().Be(0);
        }
    }
}
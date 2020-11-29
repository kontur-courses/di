using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.PointsGenerators;

namespace TagsCloudVisualizationTests.PointsGeneratorsTests
{
    [TestFixture]
    public class ArchimedesSpiralTests
    {
        [SetUp]
        public void SetUp()
        {
            sut = new ArchimedesSpiral(new Point(250, 250));
        }

        private IPointGenerator sut;

        [Test]
        public void InitArchimedesSpiral_CenterParamEqualProperty_CorrectArguments()
        {
            var center = new Point(10, 20);

            var spiral = new ArchimedesSpiral(center, 2, 0.2f);

            spiral.Center.Should().Be(center);
        }

        [TestCase(0)]
        [TestCase(0.0000f)]
        [TestCase(-0.0000000f)]
        [TestCase(0.5f, 0)]
        public void InitArchimedesSpiral_Throws_IncorrectArguments(float angleStep,
            int spiralParameter = 1)
        {
            Assert.Throws<ArgumentException>(
                () => new ArchimedesSpiral(new Point(10, 20), spiralParameter, angleStep));
        }

        [Test]
        public void GetNextPoint_Throws_IntOverflowInPointCoordinates()
        {
            var pointGenerator = new ArchimedesSpiral(new Point(int.MaxValue - 20, int.MaxValue - 20), 
                20, 20);
            
            Assert.Throws<OverflowException>(
                () =>
                {
                    for (int i = 0; i < 10; i++) 
                        pointGenerator.GetNextPoint();
                });
        }
        
        [Test]
        public void StartOverPointGenerator_ShouldResetGeneration()
        {
            var expectedPoints = new Point[3];
            for (int i = 0; i < 3; i++)
                expectedPoints[i] = sut.GetNextPoint();

            sut.StartOver();

            var actualPoints = new Point[3];
            for (int i = 0; i < 3; i++)
                actualPoints[i] = sut.GetNextPoint();

            actualPoints.Should().Equal(expectedPoints);
        }

        [TestCase(2, 0.2f, 250, 250, 252, 252, 250, 252)]
        [TestCase(-2, 0.2f, 250, 250, 248, 248, 250, 248)]
        [TestCase(2, -1f, 250, 250, 248, 252, 252, 254)]
        [TestCase(-2, -1f, 250, 250, 252, 248, 248, 246)]
        public void GetNextGeneratedPoint_ShouldConsistentlyReturnsNextSpiralPoints(int spiralParameter, float angleStep,
            params int[] expectedPointCoordinates)
        {
            var center = new Point(250, 250);
            var archimedesSpiral = new ArchimedesSpiral(center, spiralParameter, angleStep);
            var expectedPoints = new Point[3];
            for (int i = 0; i < expectedPoints.Length; i++)
                expectedPoints[i] = new Point(expectedPointCoordinates[i * 2], expectedPointCoordinates[i * 2 + 1]);

            var actualPoints = new Point[3];
            for (int i = 0; i < 3; i++)
                actualPoints[i] = archimedesSpiral.GetNextPoint();

            actualPoints.Should().Equal(expectedPoints);
        }
    }
}
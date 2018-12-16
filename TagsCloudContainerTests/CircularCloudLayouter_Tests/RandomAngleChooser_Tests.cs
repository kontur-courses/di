using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.CircularCloudLayouters;
using TagsCloudContainerTests.Extensions;

namespace TagsCloudContainerTests.CircularCloudLayouter_Tests
{
    [TestFixture]
    public class RandomAngleChooser_Tests
    {
        private RandomAngleChooser randomAngleChooser;
        private readonly Random random = new Random();
        private const double deltaAngle = Math.PI / 3;
        private const int countSteps = (int)(Math.PI * 2 / deltaAngle);

        [SetUp]
        public void SetUp()
        {
            randomAngleChooser = new RandomAngleChooser(random);
        }

        [Test]
        public void Current_ReturnsAngles_WithStepsBetween()
        {
            var angles = randomAngleChooser.GetAngles(countSteps);
            var differencesBetweenAngles = GetDifferencesBetweenAngles(angles);


            differencesBetweenAngles.Should().OnlyContain(x => Math.Abs(x - deltaAngle) <= 1e-6);
        }

        [Test]
        public void Current_ReturnsDifferentAngles_OnDifferentAnglesChosers()
        {
            var secondAngleChooser = new RandomAngleChooser(random);

            var angles = randomAngleChooser.GetAngles(10);
            var secondAngles = secondAngleChooser.GetAngles(10);

            angles.Should().NotBeEquivalentTo(secondAngles);
        }

        [Test]
        public void Current_ReturnsAngle_InRangeBetweenZeroAndTwoPI_WhenNewCircleIsStarted()
        {
            var angles = randomAngleChooser.GetAngles(7);

            angles.Last().Should().BeInRange(0, 2 * Math.PI);
        }

        [Test]
        public void Current_ReturnsAngle_InRangeBetweenZeroAndTwoPI_AtFirstTime()
        {
            var angle = randomAngleChooser.GetAngles(1).Single();

            angle.Should().BeInRange(0, 2 * Math.PI);
        }
        
        private List<double> GetDifferencesBetweenAngles(List<double> angles)
        {
            var differences = new List<double>();
            for (var index = 0; index < angles.Count - 1; index++)
                differences.Add(angles[index + 1] - angles[index]);
            return differences;
        }
    }
}
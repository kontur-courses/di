using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagCloudContainerTests
{
    public class RectangleCoordinatesCalculatorTests
    {
        [Test]
        public void CalculateRectangleCoordinates_ShouldReturnCorrectCoordinates_OnCorrectInput()
        {
            var size = new Size(100, 100);
            var center = new Point(-100, -100);
            var coordinates = RectangleCoordinatesCalculator.CalculateRectangleCoordinates(center, size);
            coordinates.Should().BeEquivalentTo(new Point(-150, -150));
        }

        [Test]
        public void CalculateRectangleCoordinates_ShouldReturnCorrectCoordinates_OnZeroSize()
        {
            var size = new Size(0, 0);
            var center = new Point(0, 0);
            var coordinates = RectangleCoordinatesCalculator.CalculateRectangleCoordinates(center, size);
            coordinates.Should().BeEquivalentTo(new Point(0, 0));
        }

        [Test]
        public void CalculateRectangleCoordinates_ShouldThrowArgumentException_WhenSizeLessThanZero()
        {
            var size = new Size(-1, -1);
            var center = new Point(0, 0);
            Action act = () => RectangleCoordinatesCalculator.CalculateRectangleCoordinates(center, size);
            act.Should().Throw<ArgumentException>().WithMessage("Incorrect size of rectangle");
        }
    }
}
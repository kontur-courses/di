using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.OldTests
{
    public class PointsRadiusComparerTests
    {
        [TestCase(0, 0, 1, 1, 2, 2, TestName = "IfFirstPointCloserToCenterTnanSecondPoint")]
        [TestCase(0, 0, 1, 2, 2, 1, TestName = "IfDistancesAreEqualAndFirstPointXLessTnanSecondPointX")]
        [TestCase(0, 0, 1, -1, 1, 1, TestName = "IfDistancesAndX-CoordinatesAreEqualAndFirstPointYLessTnanSecondPointY")]
        [TestCase(5, 5, 6, 6, 3, 3, TestName = "IfСenterIsNotZeroAndFirstPointCloserToCenterTnanSecondPoint")]
        public void Compare_ShouldReturnNegativeNumber(
            int centerX, int centerY, int firstPointX, int firstPointY, int secondPointX, int secondPointY)
        {
            var result = GetComparisonResult(centerX, centerY, firstPointX, firstPointY, secondPointX, secondPointY);

            result.Should().BeNegative();
        }

        [TestCase(0, 0, 2, 2, 1, 1, TestName = "IfSecondPointCloserToCenterTnanFirstPoint")]
        [TestCase(0, 0, 2, 1, 1, 2, TestName = "IfDistancesAreEqualAndFirstPointXBiggerThanSecondPointX")]
        [TestCase(0, 0, 1, 1, 1, -1, TestName = "IfDistancesAndX-CoordinatesAreEqualAndFirstPointYBiggerTnanSecondPointY")]
        [TestCase(5, 5, 3, 3, 6, 6, TestName = "IfСenterIsNotZeroAndSecondPointCloserToCenterTnanFirstPoint")]
        public void Compare_ShouldReturnPositiveNumber(
            int centerX, int centerY, int firstPointX, int firstPointY, int secondPointX, int secondPointY)
        {
            var result = GetComparisonResult(centerX, centerY, firstPointX, firstPointY, secondPointX, secondPointY);

            result.Should().BePositive();
        }

        [TestCase(0, 0, 1, 1, 1, 1)]
        [TestCase(0, 0, -1, -1, -1, -1)]
        [TestCase(5, 5, 2, 2, 2, 2, TestName = "AndСenterIsNotZero")]
        public void Compare_ShouldReturnZero_IfPointsAreEqual(
            int centerX, int centerY, int firstPointX, int firstPointY, int secondPointX, int secondPointY)
        {
            var result = GetComparisonResult(centerX, centerY, firstPointX, firstPointY, secondPointX, secondPointY);

            result.Should().Be(0);
        }

        private int GetComparisonResult(int centerX, int centerY, int firstPointX, int firstPointY, int secondPointX, int secondPointY)
        {
            var center = new Point(centerX, centerY);
            var firstPoint = new Point(firstPointX, firstPointY);
            var secondPoint = new Point(secondPointX, secondPointY);
            var comparer = new PointsRadiusComparer(center);

            return comparer.Compare(firstPoint, secondPoint);
        }
    }
}

using System;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.CircularCloudLayouter.CircularCloudLayouterTests
{
    [TestFixture]
    internal class ToolsTests
    {
        [TestCase(0, ExpectedResult = 0, TestName = "ReturnZenoOnZeroAngle")]
        [TestCase(Math.PI * 2, ExpectedResult = 0, TestName = "ReturnZenoWhenMultipleTwoPi1")]
        [TestCase(Math.PI * 10, ExpectedResult = 0, TestName = "ReturnZenoWhenMultipleTwoPi2")]
        [TestCase(1, ExpectedResult = 1, TestName = "ReturnEqualAngleIfAngleLessThanTwoPi_CaseFirstSector")]
        [TestCase(2, ExpectedResult = 2, TestName = "ReturnEqualAngleIfAngleLessThanTwoPi_CaseSecondSector")]
        [TestCase(4, ExpectedResult = 4, TestName = "ReturnEqualAngleIfAngleLessThanTwoPi_CaseThirdSector")]
        [TestCase(6, ExpectedResult = 6, TestName = "ReturnEqualAngleIfAngleLessThanTwoPi_CaseFourthSector")]
        public double AngleToStandardValueShould(double angle)
        {
            return angle.AngleToStandardValue();
        }

        [TestCase(1.24 + 8 * Math.PI, 1.24, TestName = "ReturnAngleEqualToResidueOfDivisionByTwoPi1_CaseFirstSector")]
        [TestCase(1.74 + 8 * Math.PI, 1.74, TestName = "ReturnAngleEqualToResidueOfDivisionByTwoPi1_CaseSecondSector")]
        [TestCase(4.10 + 8 * Math.PI, 4.10, TestName = "ReturnAngleEqualToResidueOfDivisionByTwoPi1_CaseThirdSector")]
        [TestCase(5.12 + 8 * Math.PI, 5.12, TestName = "ReturnAngleEqualToResidueOfDivisionByTwoPi1_CaseFourthSector")]
        [TestCase(Math.PI / 2 + 10 * Math.PI, Math.PI / 2, TestName =
            "ReturnAngleEqualToResidueOfDivisionByTwoPi_CasePi/2")]
        [TestCase(Math.PI + 10 * Math.PI, Math.PI, TestName = "ReturnAngleEqualToResidueOfDivisionByTwoPi_CasePi")]
        [TestCase(3 * Math.PI / 2 + 10 * Math.PI, 3 * Math.PI / 2, TestName =
            "ReturnAngleEqualToResidueOfDivisionByTwoPi_Case3Pi/2")]
        public void AngleToStandardValue_RoundingCases(double angle, double expected)
        {
            var result = angle.AngleToStandardValue();

            result.Should().BeApproximately(expected, 0.00001F);
        }

        [TestCase(0, ExpectedResult = Quadrant.First, TestName = "ReturnFirstOnAnglesFromFirstQuadrant1")]
        [TestCase(1, ExpectedResult = Quadrant.First, TestName = "ReturnFirstOnAnglesFromFirstQuadrant2")]
        [TestCase(Math.PI / 2, ExpectedResult = Quadrant.First, TestName = "ReturnFirstOnAnglesFromFirstQuadrant2")]
        [TestCase(2, ExpectedResult = Quadrant.Second, TestName = "ReturnSecondOnAnglesFromSecondQuadrant1")]
        [TestCase(Math.PI, ExpectedResult = Quadrant.Second, TestName = "ReturnSecondOnAnglesFromSecondQuadrant2")]
        [TestCase(4.14, ExpectedResult = Quadrant.Third, TestName = "ReturnThirdOnAnglesFromThirdQuadrant1")]
        [TestCase(3 * Math.PI / 2, ExpectedResult = Quadrant.Third, TestName = "ReturnThirdOnAnglesFromThirdQuadrant2")]
        [TestCase(5, ExpectedResult = Quadrant.Fourth, TestName = "ReturnFourthOnAnglesFromFourthQuadrant")]
        public Quadrant DetermineQuadrantByDirectionShould(double direction)
        {
            return direction.DetermineQuadrantByDirection();
        }
    }
}
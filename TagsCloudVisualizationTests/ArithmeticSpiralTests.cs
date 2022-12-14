using System;
using System.Drawing;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualization.CloudLayouter;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class ArithmeticSpiralTests
{
    [TestCase(0)]
    [TestCase(-4)]
    public void Constructor_NotPositiveConstant_Throw(int constant)
    {
        new Action(() => { new ArithmeticSpiral(constant); })
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Negative or zero arithmetic spiral constant not allowed");
    }


    [Test]
    public void Constructor_RightParams_NotThrow()
    {
        new Action(() => { new ArithmeticSpiral(1); })
            .Should()
            .NotThrow();
    }

    [Test]
    public void GetPoint_CalcPointByFormula_Equals()
    {
        var center = new Point(0, 0);
        const int constant = 2;
        const int length = 5;

        var arithmeticSpiral = new ArithmeticSpiral(constant);

        var point = arithmeticSpiral.GetPoint(center, length);

        var expectedX = (int)(center.X + Math.Cos(length) * length * constant);
        var expectedY = (int)(center.Y + Math.Sin(length) * length * constant);


        using (new AssertionScope())
        {
            point.X.Should().Be(expectedX);
            point.Y.Should().Be(expectedY);
        }
    }
}
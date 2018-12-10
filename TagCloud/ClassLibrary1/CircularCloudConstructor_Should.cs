using System;
using FluentAssertions;
using NUnit.Framework;
using TagCloudVisualization;

namespace TagCloudTests
{
    [TestFixture]
    public class CircularCloudConstructor_Should
    {
        [TestCase(0, 0, TestName = "is zero")]
        [TestCase(100, 100, TestName = "is in I quarter")]
        [TestCase(100, -100, TestName = "is in II quarter")]
        [TestCase(-100, 100, TestName = "is in III quarter")]
        [TestCase(-100, -100, TestName = "is in IV quarter")]
        public void NotThrow_WhenCenter(int x, int y)
        {
            // ReSharper disable once ObjectCreationAsStatement
            Action creation = () => new CircularCloudLayouter(new Point(x, y), new RoundSpiralGenerator());
            creation.Should()
                    .NotThrow();
        }
    }
}

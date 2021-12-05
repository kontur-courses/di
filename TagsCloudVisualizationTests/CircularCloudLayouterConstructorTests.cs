using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualization.Layouter;

namespace TagsCloudVisualizationTests
{
    [TestFixture] 
    public class CircularCloudLayouterConstructorTests
    {
        [Test] 
        public void CloudLayouterConstructorShouldWorkCorrectly() 
        {
            var center = new Point(2500, 2500); 
            Action creating = () => new CircularCloudLayouterForRectangles(center);
            creating.Should().NotThrow();
        }

        [TestCase(-5, 10)]
        [TestCase(5, -5)]
        [TestCase(0, 0)]
        [TestCase(10, 10)]
        public void ShouldNotThrowExceptionWithAnySize(int width, int height)
        {
            var layouterCenter = new Point(width, height);
            Action creating = () => new CircularCloudLayouterForRectangles(layouterCenter);
            creating.Should().NotThrow();
        }
    }
}

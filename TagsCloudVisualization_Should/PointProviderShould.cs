using FluentAssertions;
using NUnit.Framework;
using System;
using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Should
{
    public class PointProviderShould
    {
        private IConfig config;

        [SetUp]
        public void SetUp()
        {
            config = new Config();
            config.SetValues(new Font(FontFamily.GenericMonospace, 25), 
                new Point(1500, 1500), Color.Blue);
        }
        
        [Test]
        public void GetPoint_ReturnPoint_AfterCallingMethod()
        {
            var center = new Point(500, 500);
            config.SetValues(new Font(FontFamily.GenericMonospace, 25), 
                center, Color.Blue);
            var pointProvider = new PointProvider(config);
            var expectedPoint = new Point(500, 500);

            var actualPoint = pointProvider.GetPoint();

            actualPoint.ShouldBeEquivalentTo(expectedPoint);
        }

        [Test]
        public void CreatePointProvider_ThrowArgumentException_CenterWithNegativeXOrY()
        {
            var center = new Point(-1, -1);
            config.SetValues(new Font(FontFamily.GenericMonospace, 25), 
                center, Color.Blue);
            Action act = () => new PointProvider(config);

            act.ShouldThrow<ArgumentException>().WithMessage("X or Y of center was negative");
        }
    }
}

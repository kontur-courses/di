using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud;
using TagsCloud.Interfaces;

namespace TagsCloudTests
{
    [TestFixture]
    public class RectangleComposerShould
    {
        private IServiceProvider serviceProvider;
        private RectangleComposer rectangleComposer;
        private ISpiral spiral;

        [SetUp]
        public void SetUp()
        {
            serviceProvider = ContainerBuilder.GetNewTagCloudServices(0, 0);
            rectangleComposer = serviceProvider.GetService<RectangleComposer>();
            spiral = rectangleComposer.Spiral;
        }

        [Test]
        public void FindFreePlaceOnSpiral_WhenNoFreePlace_ShouldExpandSpiral()
        {
            var beginSpiralLength = spiral.Points.Count;
            var rect = new Rectangle(0, 0, 10, 10);

            rectangleComposer.FindFreePlaceOnSpiral(rect);
            rectangleComposer.FindFreePlaceOnSpiral(rect);
            rectangleComposer.FindFreePlaceOnSpiral(rect);

            spiral.Points.Count.Should().BeGreaterThan(beginSpiralLength);
        }

        [TestCase(100, 100)]
        [TestCase(-100, 100)]
        [TestCase(-100, -100)]
        [TestCase(100, -100)]
        public void MoveToCenter_SingleRectAside_ShouldMoveToCenter(int posX, int posY)
        {
            var rect = new Rectangle(posX, posY, 10, 10);

            var offsetRect = rectangleComposer.MoveToCenter(rect);
            var offsetRectCenterX = offsetRect.X + offsetRect.Width / 2;
            var offsetRectCenterY = offsetRect.Y + offsetRect.Height / 2;

            Math.Abs(offsetRectCenterX).Should().BeLessThan(RectangleComposer.CenterAreaRadius);
            Math.Abs(offsetRectCenterY).Should().BeLessThan(RectangleComposer.CenterAreaRadius);
        }

        [Test]
        public void GetNextPointToCenter_ZeroAngle_PointShouldBeZero()
        {
            var expectedPoint = new Point(RectangleComposer.StepToCenter, 0);

            var nextPoint = rectangleComposer.GetNextPointToCenter(0);

            nextPoint.Should().Be(expectedPoint);
        }

        [Test]
        public void IsRectangleInCenter_PointInCenterArea_ShouldReturnTrue()
        {
            var center = new Point(0, 0);
            var rect = new Rectangle(-5, -5, 5, 5);

            var checkLocationInArea = RectangleComposer.IsRectangleInCenter(rect, center);

            checkLocationInArea.Should().BeTrue();
        }

        [Test]
        public void IsRectangleInCenter_PointNotInCenterArea_ShouldReturnFalse()
        {
            var center = Point.Empty;
            var rect = new Rectangle(15, 5, 10, 10);

            var checkLocationInArea = RectangleComposer.IsRectangleInCenter(rect, center);

            checkLocationInArea.Should().BeFalse();
        }

        [Test]
        public void IsRectangleIntersectOther_CommonData_ShouldReturnTrue()
        {
            var rectangle = new Rectangle(100, 100, 10, 10);
            var rects = new List<Rectangle>
            {
                new Rectangle(0, 0, 50, 50),
                new Rectangle(-100, -100, 10, 10),
                new Rectangle(200, 200, 50, 50),
                new Rectangle(30, 30, 5, 5)
            };

            var intersectFlag = RectangleComposer.IsRectangleIntersectOther(rectangle, rects);

            intersectFlag.Should().BeFalse();
        }

        [Test]
        public void IsRectangleIntersectOther_IntersectsRects_ShouldReturnFalse()
        {
            var rectangle = new Rectangle(100, 100, 100, 100);
            var rects = new List<Rectangle>
            {
                new Rectangle(0, 0, 125, 125),
            };

            var intersectFlag = RectangleComposer.IsRectangleIntersectOther(rectangle, rects);

            intersectFlag.Should().BeTrue();
        }
    }
}

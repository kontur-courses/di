using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class CircularCloudVisualizer_Should
    {
        private CircularCloudLayouter layout;
        private CircularCloudVisualizer visualizer;
        private List<Rectangle> rectangles;

        [SetUp]
        public void SetUp()
        {
            layout = new CircularCloudLayouter();
            rectangles = new List<Rectangle>();
            visualizer = new CircularCloudVisualizer(rectangles);
        }

        [Test]
        public void DrawRectangles_AddSingleRectangle_DefaultSizes()
        {
            var newRectangle = layout.PutNextRectangle(new Size(100, 100));
            rectangles.Add(newRectangle);
            visualizer.GetTagCloudImage().Size.Should().Be(new Size(500, 500));
        }

        private static int GetRectangleRadius(Rectangle newRectangle)
        {
            return (int)Math.Sqrt(Math.Pow(newRectangle.Right, 2) + Math.Pow(newRectangle.Top, 2));
        }

        [Test]
        public void GetGetCircumscribedСircleRadius_AddFirstRectangle_CorrectCircleRadius()
        {
            var newRectangle = layout.PutNextRectangle(new Size(200, 200));
            var radius = GetRectangleRadius(newRectangle);
            newRectangle.GetCircumcircleRadius().Should().Be(radius);
        }


        [Test]
        public void ShiftRectangleToCenter_ShiftSingleRectangle_CorrectShift()
        {
            var newRectangle = layout.PutNextRectangle(new Size(300, 300));
            visualizer.ShiftRectangleToCenter(newRectangle).Should().Be(new Rectangle(-150, -150, 300, 300));
        }
    }
}
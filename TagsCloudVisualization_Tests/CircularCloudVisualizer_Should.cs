using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.TagCloudVisualization.Extensions;
using TagCloud.TagCloudVisualization.Layouter;
using TagCloud.TagCloudVisualization.Visualization;

namespace TagsCloudVisualization_Tests
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
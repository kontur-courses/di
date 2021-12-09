using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization;
using TagsCloudVisualizationTests.Interfaces;
using TagsCloudVisualizationTests.TestingLibrary.RectangleStyles;

namespace TagsCloudVisualizationTests.TestingLibrary
{
    public class RectangleVisualizer : IVisualizer
    {
        public IRectangleStyle Style { get; set; } = new RedPenStyle();

        private readonly List<Rectangle> rectangles;

        public RectangleVisualizer(IEnumerable<Rectangle> rectangles)
        {
            var rectanglesList = rectangles.ToList();
            if (!rectanglesList.Any())
                throw new ArgumentException("Collection is empty.", nameof(rectangles));

            this.rectangles = rectanglesList;
        }

        public void Draw(Graphics graphics)
        {
            var offset = PointHelper.GetTopLeftCorner(rectangles.Select(rectangle => rectangle.Location));
            rectangles
                .Select(
                    rectangle =>
                        new Rectangle(new Point(rectangle.Left - offset.X, rectangle.Top - offset.Y), rectangle.Size))
                .ToList()
                .ForEach(rectangle => Style.Draw(graphics, rectangle));
        }

        public Size GetBitmapSize()
        {
            var topLeft = PointHelper.GetTopLeftCorner(rectangles.Select(rectangle => rectangle.Location));
            var bottomRight = PointHelper.GetBottomRightCorner(
                rectangles
                    .Select(rectangle => new Point(rectangle.Right, rectangle.Bottom)));

            return new Size(
                bottomRight.X - topLeft.X + 1,
                bottomRight.Y - topLeft.Y + 1);
        }
    }
}
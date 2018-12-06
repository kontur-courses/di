using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.TagsCloud.CircularCloud;

namespace TagsCloudVisualization.TagsCloud.CloudConstruction
{
    public class CloudCompactor
    {
        public CircularCloudLayouter Cloud { get; set; }

        public CloudCompactor(CircularCloudLayouter cloud)
        {
            Cloud = cloud;
        }

        public Rectangle ShiftRectangleToTheNearest(Rectangle rectangle)
        {
            if (Cloud.Rectangles.Count == 0)
                return rectangle;
            var yLevelRectangles = Cloud.Rectangles.Where(rect => !(rectangle.IsBelowAnother(rect)
                                                                  || rectangle.IsAboveAnother(rect))).ToList();
            rectangle = FindNearestRectangleHorizontally(rectangle, yLevelRectangles);
            var xLevelRectangles = Cloud.Rectangles.Where(rect => !(rectangle.ToRightOfAnother(rect)
                                                                  || rectangle.ToLeftOfAnother(rect))).ToList();
            rectangle = FindNearestRectangleVertically(rectangle, xLevelRectangles);

            return rectangle;
        }

        private Rectangle FindNearestRectangleHorizontally(Rectangle rectangle, List<Rectangle> yLevelRectangles)
        {
            if (yLevelRectangles.Count == 0) return rectangle;
            int distanceToNearestRectangle;
            if (rectangle.X <= Cloud.Center.X)
            {
                var rectangles = yLevelRectangles.Where(rec => rec.ToRightOfAnother(rectangle)).ToList();
                distanceToNearestRectangle = rectangles.Count == 0
                    ? 0
                    : rectangles
                        .Min(rec => Math.Abs(rec.X - (rectangle.X + rectangle.Width)));
            }
            else
            {
                var correctRectangles = yLevelRectangles.Where(rec => rec.ToLeftOfAnother(rectangle)).ToList();
                distanceToNearestRectangle = correctRectangles.Count == 0
                    ? 0
                    : -correctRectangles
                        .Min(rec => Math.Abs(rec.X + rec.Width - rectangle.X));
            }
            var newLocation = new Point(rectangle.X + distanceToNearestRectangle, rectangle.Y);
            rectangle = new Rectangle(newLocation, rectangle.Size);

            return rectangle;
        }

        private Rectangle FindNearestRectangleVertically(Rectangle rectangle, List<Rectangle> xLevelRectangles)
        {
            if (xLevelRectangles.Count == 0) return rectangle;
            int distanceToNearestRectangle;
            if (rectangle.Y >= Cloud.Center.Y)
            {
                var correctRectangles = xLevelRectangles.Where(rec => rec.IsAboveAnother(rectangle)).ToList();
                distanceToNearestRectangle = correctRectangles.Count == 0
                    ? 0
                    : -correctRectangles
                        .Min(rec => Math.Abs(rec.Y + rec.Height - rectangle.Y));
            }
            else
            {
                var correctRectangles =
                    xLevelRectangles.Where(rec => rec.IsBelowAnother(rectangle)).ToList();
                distanceToNearestRectangle = correctRectangles.Count == 0
                    ? 0
                    : correctRectangles
                        .Min(rec => Math.Abs(rec.Y - rectangle.Y - rectangle.Height));
            }
            var newLocation = new Point(rectangle.X, rectangle.Y + distanceToNearestRectangle);
            rectangle = new Rectangle(newLocation, rectangle.Size);
            return rectangle;
        }
    }
}
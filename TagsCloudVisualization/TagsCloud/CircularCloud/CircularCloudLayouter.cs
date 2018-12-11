using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.TagsCloud.CloudConstruction;

namespace TagsCloudVisualization.TagsCloud.CircularCloud
{
    public class CircularCloudLayouter
    {
        public Size WindowSize { get; set; }
        public Point Center { get; set; }
        public List<Rectangle> Rectangles { get; set; }
        public CloudCompactor CloudCompactor { get; set; }
        public RectangleGenerator RectangleGenerator { get; set; }
        public static bool IsCompressedCloud { get; set; }

        public CircularCloudLayouter(Point center, Size windowSize)
        {
            WindowSize = windowSize;
            if (center.X < 0 || center.Y < 0 || center.X > WindowSize.Width || center.Y > WindowSize.Height)
                throw new ArgumentException("Center coordinates must not exceed the window size");
            Center = center;
            Rectangles = new List<Rectangle>();
            CloudCompactor = new CloudCompactor(this);
            RectangleGenerator = new RectangleGenerator(this);
        }

        public Rectangle PutNextRectangle(Size size)
        {
            if (size.Height < 0 || size.Width < 0)
                throw new ArgumentException("Size should be positive");
            var resultRect = RectangleGenerator.GetNextRectangle(size);
            if (IsCompressedCloud)
                resultRect = CloudCompactor.ShiftRectangleToTheNearest(resultRect);
            Rectangles.Add(resultRect);
            return resultRect;
        }
    }
}
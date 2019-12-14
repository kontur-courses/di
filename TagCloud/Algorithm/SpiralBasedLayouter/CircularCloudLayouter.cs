using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure;

namespace TagCloud.Algorithm.SpiralBasedLayouter
{
    public class CircularCloudLayouter : ITagCloudLayouter
    {
        public Point Center => pictureConfig.Center;

        private readonly ISpiral spiral;
        private readonly List<Rectangle> rectangles;
        private readonly PictureConfig pictureConfig;
        
        public CircularCloudLayouter(ISpiral spiral, PictureConfig pictureConfig)
        {
            this.spiral = spiral;
            this.pictureConfig = pictureConfig;
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            while (true)
            {
                var currentPoint = spiral.GetNextPoint();
                var rectangle = RectangleUtils.GetClosestRectangleThatDoesNotIntersectWithOthers(
                    currentPoint, rectangleSize, Center, rectangles);
                if (rectangle == null)
                    continue;
                rectangles.Add(rectangle.Value);
                return rectangle.Value;
            }
        }
    }
}

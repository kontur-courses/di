using System.Drawing;
using WordCloudImageGenerator.LayoutCraetion.Layouters;
using WordCloudImageGenerator.Layouting.Cloud;

namespace WordCloudImageGenerator.Layouting.Layouters.Linear
{
    class LinearLayouter:ICloudLayouter
    {
        private Rectangle surface;
        private Point position;
        private int lineHeight;
        private IRectangleCloud rectangleCloud;
        private const int SurfaceLengthFactor = 10;
        private const int SurfaceHeightFactor = 20;
        public LinearLayouter()
        {
            position = new Point(0,0);
            this.rectangleCloud = new RectangleCloud();
        }
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (surface.Width == 0 && surface.Height == 0)
                AdjustGeneration(rectangleSize);

            var foundRectangle = new Rectangle(position, rectangleSize);
            if (HorizontalOverflow(foundRectangle))
                foundRectangle = LineFeed(foundRectangle);
            position = new Point(foundRectangle.Right, foundRectangle.Y);

            rectangleCloud.Rectangles.Add(foundRectangle);
            return foundRectangle;
        }

        private Size AdjustGeneration(Size rectangleSize)
        {
            lineHeight = rectangleSize.Height;
            surface.Width = rectangleSize.Width * SurfaceLengthFactor;
            position = new Point(0, surface.Location.Y);
            surface.Height = rectangleSize.Height * SurfaceHeightFactor;
            return rectangleSize;
        }

        private Rectangle LineFeed(Rectangle rectangle)
        {
            Rectangle result = new Rectangle(new Point(0, position.Y + lineHeight), rectangle.Size);
            lineHeight = rectangle.Height;
            return result;
        }

        private bool HorizontalOverflow(Rectangle rectangle) => rectangle.Right > surface.Right;

        public void Reset() => rectangleCloud = new RectangleCloud();
    }
}
using System.Drawing;
using WordCloudImageGenerator.LayoutCraetion.Cloud;

namespace WordCloudImageGenerator.LayoutCraetion.Layouters.Linear
{
    class LinearLayouter:ICloudLayouter
    {
        private Rectangle surface;
        private Point position;
        private int lineHeight;
        public IRectangleCloud RectangleCloud;
        private const int surfaceLengthFactor = 10;
        private const int surfaceHeightFactor = 20;
        public LinearLayouter()
        {
            position = new Point(0,0);
            this.RectangleCloud = new RectangleCloud();
        }
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (surface.Width == 0 && surface.Height == 0)
                AdjustGeneration(rectangleSize);

            var foundRectangle = new Rectangle(position, rectangleSize);
            if (HorizontalOverflow(foundRectangle))
                foundRectangle = LineFeed(foundRectangle);
            position = new Point(foundRectangle.Right, foundRectangle.Y);

            RectangleCloud.Rectangles.Add(foundRectangle);
            return foundRectangle;
        }

        private Size AdjustGeneration(Size rectangleSize)
        {
            lineHeight = rectangleSize.Height;
            surface.Width = rectangleSize.Width * surfaceLengthFactor;
            position = new Point(0, surface.Location.Y);
            surface.Height = rectangleSize.Height * surfaceHeightFactor;
            return rectangleSize;
        }

        private Rectangle LineFeed(Rectangle rectangle)
        {
            Rectangle result = new Rectangle(new Point(0, position.Y + lineHeight), rectangle.Size);
            lineHeight = rectangle.Height;
            return result;
        }

        private bool HorizontalOverflow(Rectangle rectangle) => rectangle.Right > surface.Right;

        public void Reset() => this.RectangleCloud = new RectangleCloud();
    }
}
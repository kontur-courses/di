using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.CircularCloudLayouter
{
    public class CircularCloudLayout: IRectangleLayout
    {
        private readonly RectangleStorage _rectangleStorage;
        private readonly List<Rectangle> _rectangles;

        public CircularCloudLayout(RectangleStorage rectangleStorage)
        {
            _rectangleStorage = rectangleStorage;
            _rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var result = _rectangleStorage.PlaceNewRectangle(rectangleSize);
            _rectangles.Add(result);

            return result;
        }

        public List<Rectangle> GetRectangles()
        {
            return _rectangles;
        }

        public IEnumerable<Rectangle> GetCoordinatesToDraw()
        {
            var result = new List<Rectangle>();
            var minX = _rectangles.GetMinX();
            var yHeight = _rectangles.GetYHeight();

            foreach (var rectangle in _rectangles)
            {
                var xShift = -minX;
                var yShift = -yHeight;
                var x = rectangle.X + xShift;
                var y = rectangle.Y + yShift - rectangle.Height;

                result.Add(new Rectangle(x, y, rectangle.Width, rectangle.Height));
            }

            return result;
        }

        public Size ImageSize()
        {
            return _rectangles.GetImageSize();
        }
    }
}
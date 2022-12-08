using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud
{
    public class CircularCloudLayouter
    {
        public RectangleComposer Composer { get; set; }

        public CircularCloudLayouter(Point center) // в конструкторе это позиция центра облака тегов
        {
            var rectangles = new List<Rectangle>();
            Composer = new RectangleComposer(rectangles, center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (CheckSizeAvailable(rectangleSize))
            {
                var placedRectangle = Composer.GetNextRectangleInCloud(rectangleSize);
                return placedRectangle;
            }

            return Rectangle.Empty;
        }

        public bool CheckSizeAvailable(Size checkAvailable)
        {
            return (checkAvailable.Width > 0 && checkAvailable.Height > 0);
        }
    }
}
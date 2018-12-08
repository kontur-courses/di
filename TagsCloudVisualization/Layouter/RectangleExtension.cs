using System.Drawing;

namespace TagsCloudVisualization.Layouter
{
    public static class RectangleExtension
    {
        public static Rectangle ShiftRectangleToTopLeftCorner(this Rectangle rectangle) =>
            new Rectangle(rectangle.Location.Add(new Point(-rectangle.Width / 2, -rectangle.Height / 2)), rectangle.Size);
        

        public static Rectangle ShiftRectangleToBitMapCenter(this Rectangle rectangle, Bitmap bitmap) =>
            new Rectangle(rectangle.Location.Add(new Point(bitmap.Width / 2, bitmap.Height/2)), rectangle.Size);
        
    }
}

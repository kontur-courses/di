using TagCloudContainer.Models;

namespace TagCloudGUI
{
    public static class ImageSizer
    {
        public static Size GetImageSize(IEnumerable<RectangleWithText> rectangles)
        {
            var maxLocationX = GetMaxPointLocation(rectangles, x => Math.Abs(x.Rectangle.X));
            var maxLocationY = GetMaxPointLocation(rectangles, y => Math.Abs(y.Rectangle.Y));
            
            return new Size(
                (Math.Abs(maxLocationX.Rectangle.Location.X) + maxLocationX.Rectangle.Height) * 3,
                (Math.Abs(maxLocationY.Rectangle.Location.Y) + maxLocationY.Rectangle.Height) * 3);
        }

        private static RectangleWithText GetMaxPointLocation(IEnumerable<RectangleWithText> rectangles,
            Func<RectangleWithText, int> func)
        {
            return rectangles.MaxBy(func);
        }
    }
}

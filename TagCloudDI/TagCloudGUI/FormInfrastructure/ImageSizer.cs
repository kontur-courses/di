using TagCloudContainer.Models;

namespace TagCloudGUI
{
    public static class ImageSizer
    {
        public static Size GetImageSize(IEnumerable<RectangleWithText> rectangles)
        {
            var maxLocationX = GetMaxPointLocation(rectangles, x => Math.Abs(x.rectangle.X));
            var maxLocationY = GetMaxPointLocation(rectangles, y => Math.Abs(y.rectangle.Y));
            
            return new Size(
                (Math.Abs(maxLocationX.rectangle.Location.X) + maxLocationX.rectangle.Height) * 3,
                (Math.Abs(maxLocationY.rectangle.Location.Y) + maxLocationY.rectangle.Height) * 3);
        }

        private static RectangleWithText GetMaxPointLocation(IEnumerable<RectangleWithText> rectangles,
            Func<RectangleWithText, int> func)
        {
            return rectangles.MaxBy(func);
        }
    }
}

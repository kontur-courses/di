namespace TagCloudGraphicalUserInterface
{
    public static class ImageSizer
    {
        public static Size GetImageSize(IEnumerable<TextRectangle> rectangles, Point offsetPoint)
        {
            var maxLocationX = GetMaxPointLocation(rectangles, x => Math.Abs(x.rectangle.X));
            var maxLocationY = GetMaxPointLocation(rectangles, y => Math.Abs(y.rectangle.Y));
            return new Size(
                (Math.Abs(maxLocationX.rectangle.Location.X) + maxLocationX.rectangle.Height) * 3 +
                Math.Abs(offsetPoint.X) * 2,
                (Math.Abs(maxLocationY.rectangle.Location.Y) + maxLocationY.rectangle.Height) * 3 +
                Math.Abs(offsetPoint.Y) * 2);
        }

        private static TextRectangle GetMaxPointLocation(IEnumerable<TextRectangle> rectangles,
            Func<TextRectangle, int> func)
        {
            return rectangles.MaxBy(func);
        }
    }
}

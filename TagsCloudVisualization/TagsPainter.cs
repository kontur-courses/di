using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TagsPainter
    {
        private readonly Random random;
        public int HeightOffset { get; }
        public int WidthOffset { get; }

        public TagsPainter(int widthOffset = 200, int heightOffset = 200)
        {
            ValidateOffsets(widthOffset, heightOffset);
            WidthOffset = widthOffset;
            HeightOffset = heightOffset;
            random = new Random();
        }

        public void SaveToFile(string filePath, Rectangle[] rectangles)
        {
            if (rectangles.Length == 0)
                throw new ArgumentException("Impossible to save an empty layout");

            var drawingParams = CalculateDrawingParams(rectangles);

            using var bitmap = new Bitmap(drawingParams.BitmapWidth, drawingParams.BitmapHeight);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Black);
            foreach (var rectangle in rectangles)
            {
                var brush = new SolidBrush(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));
                rectangle.Offset(drawingParams.DrawingOffset);

                graphics.FillRectangle(brush, rectangle);
            }

            bitmap.Save(filePath, ImageFormat.Png);
        }

        private DrawingParams CalculateDrawingParams(Rectangle[] rectangles)
        {
            var maxX = rectangles.Max(x => x.X + x.Size.Width);
            var maxY = rectangles.Max(x => x.Y);
            var minX = rectangles.Min(x => x.X);
            var minY = rectangles.Min(x => x.Y - x.Size.Height);
            var width = maxX - minX;
            var height = maxY - minY;

            return new DrawingParams
            {
                BitmapWidth = width + WidthOffset,
                BitmapHeight = height + HeightOffset,
                DrawingOffset = new Point(width / 2 + WidthOffset / 2, height / 2 + HeightOffset / 2)
            };
        }

        private static void ValidateOffsets(int widthOffset, int heightOffset)
        {
            if (widthOffset < 0 || heightOffset < 0)
                throw new ArgumentException("Offsets must be great or equal to zero");
        }

        private struct DrawingParams
        {
            public int BitmapWidth;
            public int BitmapHeight;
            public Point DrawingOffset;
        }
    }
}
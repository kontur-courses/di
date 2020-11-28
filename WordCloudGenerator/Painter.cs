using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace WordCloudGenerator
{
    public class Painter
    {
        private readonly FontFamily fontFamily;
        private readonly ImageFormat imageFormat;
        private readonly ILayouter layouter;
        private readonly IPalette palette;
        private Size imgSize;

        public Painter(ImageFormat imageFormat, ILayouter layouter, FontFamily fontFamily, IPalette palette,
            Size imgSize = default)
        {
            this.imageFormat = imageFormat;
            this.layouter = layouter;
            this.fontFamily = fontFamily;
            this.palette = palette;
            this.imgSize = imgSize;
        }

        public Bitmap Paint(Dictionary<string, int> words)
        {
            FillLayouter(words);
            var layoutRects = layouter.GetRectangles();

            if (imgSize == default)
                imgSize = CalculateImageSize(layoutRects);

            var bitmap = new Bitmap(imgSize.Width, imgSize.Height);
            using var g = Graphics.FromImage(bitmap);
            var rectIndex = 0;
            var shiftVector = CalculateShiftVector(layoutRects);

            foreach (var (word, fontSize) in words)
            {
                var font = new Font(fontFamily, fontSize);
                using var brush = new SolidBrush(palette.GetNextColor());
                g.DrawString(word, font, brush, layoutRects[rectIndex].Shift(shiftVector));
                rectIndex++;
            }

            return bitmap;
        }

        private void FillLayouter(Dictionary<string, int> words)
        {
            using var gForMeasure = Graphics.FromImage(new Bitmap(1, 1));
            foreach (var (word, fontSize) in words)
            {
                var font = new Font(fontFamily, fontSize);
                var size = gForMeasure.MeasureString(word, font);
                layouter.PutNextRectangle(size);
            }
        }

        public void SaveImage(Image img, string path)
        {
            img.Save(path, imageFormat);
        }

        private static Size CalculateImageSize(RectangleF[] rectangles)
        {
            var maxX = (int) rectangles.Max(rect => rect.X + rect.Width);
            var minX = (int) rectangles.Min(rect => rect.X);
            var maxY = (int) rectangles.Max(rect => rect.Y + rect.Height);
            var minY = (int) rectangles.Min(rect => rect.Y);

            var width = maxX - minX;
            var height = maxY - minY;

            return new Size(width, height);
        }

        private static Point CalculateShiftVector(RectangleF[] rectangles)
        {
            var minX = (int) rectangles.Min(rect => rect.X);
            var minY = (int) rectangles.Min(rect => rect.Y);

            return new Point(-minX, -minY);
        }
    }
}
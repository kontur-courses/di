using System.Drawing;
using System.Linq;
using TagCloudContainer.Api;

namespace TagCloudContainer
{
    public class LayoutVisualizer : IRectangleVisualizer
    {
        private ICloudLayouter Layouter;

        public LayoutVisualizer(ICloudLayouter layouter)
        {
            Layouter = layouter;
        }

        public Image CreateImageWithRectangles(DrawingOptions options)
        {
            var bmp = CreateSizedBitmap();
            var graphics = Graphics.FromImage(bmp);
            FillBackground(graphics, bmp, options);

            DrawRectanglesOnImage(options, graphics, bmp);

            graphics.Flush();
            return bmp;
        }

        private void DrawRectanglesOnImage(DrawingOptions options,
            Graphics graphics, Image img)
        {
            foreach (var rect in Layouter.Layout)
            {
                rect.Offset(new Point(img.Width / 2, img.Height / 2));
                graphics.DrawRectangle(options.Pen, rect);
            }
        }

        private Bitmap CreateSizedBitmap()
        {
            int maxX = Layouter.Layout.Select(r => r.Right).Max();
            int minX = Layouter.Layout.Select(r => r.Left).Min();
            int maxY = Layouter.Layout.Select(r => r.Bottom).Max();
            int minY = Layouter.Layout.Select(r => r.Top).Min();

            int bmpWidth = maxX - minX;
            int bmpHeight = maxY - minY;

            return new Bitmap(bmpWidth, bmpHeight);
        }


        private static void FillBackground(Graphics graphics, Image img, DrawingOptions options)
        {
            graphics.FillRegion(options.BackgroundBrush, new Region(new Rectangle(0, 0, img.Width, img.Width)));
        }
    }
}
using System.Drawing;
using System.Linq;
using TagCloudContainer.Api;

namespace TagCloudContainer
{
    public class LayoutVisualizer : IRectangleVisualizer
    {
        public ICloudLayouter Layouter;

        public LayoutVisualizer(ICloudLayouter layouter)
        {
            Layouter = layouter;
        }

        public Image CreateImageWithRectangles(DrawingOptions options)
        {
            var bmp = CreateSizedBitmapForLayouter(Layouter);
            var graphics = Graphics.FromImage(bmp);
            FillBackground(graphics, bmp, options);

            DrawRectanglesOnImage(Layouter, options, graphics, bmp);

            graphics.Flush();
            return bmp;
        }

        private static void DrawRectanglesOnImage(ICloudLayouter layouter, DrawingOptions options,
            Graphics graphics, Image img)
        {
            foreach (var rect in layouter.Layout)
            {
                rect.Offset(new Point(img.Width / 2, img.Height / 2));
                graphics.DrawRectangle(options.Pen, rect);
            }
        }

        public Bitmap CreateSizedBitmapForLayouter(ILayoutProvider layouter)
        {
            int maxX = layouter.Layout.Select(r => r.Right).Max();
            int minX = layouter.Layout.Select(r => r.Left).Min();
            int maxY = layouter.Layout.Select(r => r.Bottom).Max();
            int minY = layouter.Layout.Select(r => r.Top).Min();

            int bmpWidth = maxX - minX;
            int bmpHeight = maxY - minY;

            return new Bitmap(bmpWidth, bmpHeight);
        }

        private void FillBackground(Graphics graphics, Image img, DrawingOptions options)
        {
            graphics.FillRegion(options.BackgroundBrush, new Region(new Rectangle(0, 0, img.Width, img.Width)));
        }
    }
}
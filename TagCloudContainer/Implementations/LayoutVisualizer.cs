using System.Drawing;
using System.Linq;
using TagCloudContainer.Api;

namespace TagCloudContainer.Implementations
{
    public class LayoutVisualizer : IRectangleVisualizer
    {
        private ICloudLayouter layouter;
        private IRectanglePenProvider penProvider;
        private DrawingOptions options;

        public LayoutVisualizer(ICloudLayouter layouter, IRectanglePenProvider penProvider, DrawingOptions options)
        {
            this.layouter = layouter;
            this.options = options;
            this.penProvider = penProvider;
        }

        public Image CreateImageWithRectangles()
        {
            var bmp = CreateSizedBitmap();
            var graphics = Graphics.FromImage(bmp);
            FillBackground(graphics, bmp, options);

            DrawRectanglesOnImage(graphics, bmp);

            graphics.Flush();
            return bmp;
        }

        private void DrawRectanglesOnImage(Graphics graphics, Image img)
        {
            foreach (var rect in layouter.Layout)
            {
                rect.Offset(new Point(img.Width / 2, img.Height / 2));
                graphics.DrawRectangle(penProvider.CreatePenForRectangle(rect), rect);
            }
        }

        private Bitmap CreateSizedBitmap()
        {
            int maxX = layouter.Layout.Select(r => r.Right).Max();
            int minX = layouter.Layout.Select(r => r.Left).Min();
            int maxY = layouter.Layout.Select(r => r.Bottom).Max();
            int minY = layouter.Layout.Select(r => r.Top).Min();

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
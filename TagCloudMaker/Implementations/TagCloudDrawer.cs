using System.Collections.Generic;
using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud.Implementations
{
    public class TagCloudDrawer: ITagCloudDrawer
    {
        public Image DrawTagCloud(IEnumerable<TextRectangle> rectangles, DrawingSettings settings)
        {
            var canvasBitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            var canvas = Graphics.FromImage(canvasBitmap);
            canvas.Clear(settings.Background);

            foreach (var rectangle in rectangles)
            {
                var fontSize = PickUpFontSize(rectangle, settings.FontFamily);
                canvas.DrawString(rectangle.Text, new Font(settings.FontFamily, fontSize), new SolidBrush(settings.BrushColor), 
                    rectangle.Rectangle);
            }

            canvas.Save();
            return canvasBitmap;
        }

        private int PickUpFontSize(TextRectangle tr, FontFamily family)
        {
            var fits = false;
            var size = tr.Rectangle.Height;
            using (var image = new Bitmap(1, 1))
            {
                using (var g = Graphics.FromImage(image))
                {
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    while (!fits)
                    {
                        var font = new Font(family, size);
                        var stringSize = g.MeasureString(tr.Text, font);
                        fits = stringSize.Width < tr.Rectangle.Width;
                        size -= 1;
                    }
                }
            }
            return size;
        }
    }
}

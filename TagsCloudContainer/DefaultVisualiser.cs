using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer
{
    public class DefaultVisualiser : IVisualiser
    {
        private Size sizeOfBitmap;
        private Color fontColor;
        private Font font;

        public DefaultVisualiser(Size size, String color, String font)
        {
            sizeOfBitmap = size;
            fontColor = Color.FromName(color);
            this.font = new Font(font, 50);
        }

        public Bitmap DrawRectangles(ICloudLayouter ccl, (string, Size)[] arr)
        {
            var bitmapWidth = sizeOfBitmap.Width;
            var bitmapHeight = sizeOfBitmap.Height;

            var bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            Graphics g = Graphics.FromImage(bitmap);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            var brush = new SolidBrush(fontColor);
            var pen = new Pen(fontColor, 4);
            g.Clear(Color.White);
            for (var i = 0; i < ccl.RectanglesList.Count; i++)
            {
                var rect = ccl.RectanglesList[i];
                var font = GetAdjustedFont(g, arr[i].Item1, this.font, arr[i].Item2.Width, 
                    300, 1, true);
                g.DrawString(arr[i].Item1, font, brush, rect);
                g.DrawRectangle(pen, rect);
            }

            return bitmap;
        }

        public static Font GetAdjustedFont(Graphics g, string graphicString, Font originalFont, int containerWidth,
            int maxFontSize, int minFontSize, bool smallestOnFail)
        {
            Font testFont = null;
            for (int adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize--)
            {
                testFont = new Font(originalFont.Name, adjustedSize, originalFont.Style);

                SizeF adjustedSizeNew = g.MeasureString(graphicString, testFont);

                if (containerWidth > Convert.ToInt32(adjustedSizeNew.Width))
                {
                    return testFont;
                }
            }

            if (smallestOnFail)
            {
                return testFont;
            }
            else
            {
                return originalFont;
            }
        }
    }
}
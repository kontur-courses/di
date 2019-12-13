using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer
{
    public class SimpleRectanglesVisualiser : IVisualiser
    {
        private Size SizeOfBitmap;
        
        public SimpleRectanglesVisualiser(Size size)
        {
            SizeOfBitmap = size;
        }

        public Bitmap DrawRectangles(ICloudLayouter ccl, (string, Size)[] arr)
        {
            var bitmapWidth = SizeOfBitmap.Width;
            var bitmapHeight = SizeOfBitmap.Height;
            
            var bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            Graphics g = Graphics.FromImage(bitmap);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var brush = new SolidBrush(Color.White);
            var pen = new Pen(Color.Black, 4);
            g.Clear(Color.White);
            for (var i = 0; i < ccl.RectanglesList.Count; i++)
            {
                var rect = ccl.RectanglesList[i];
                var font = GetAdjustedFont(g, arr[i].Item1, new Font("Tahoma", 8), arr[i].Item2.Width,100, 1, true);
                g.DrawString(arr[i].Item1, font, Brushes.Black, rect);
                g.DrawRectangle(pen, rect);
            }
            return bitmap;
        }
        
        public static Font GetAdjustedFont(Graphics g, string graphicString, Font originalFont, int containerWidth, int maxFontSize, int minFontSize, bool smallestOnFail)
        {
            Font testFont = null;
            // We utilize MeasureString which we get via a control instance           
            for (int adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize--)
            {
                testFont = new Font(originalFont.Name, adjustedSize, originalFont.Style);

                // Test the string with the new size
                SizeF adjustedSizeNew = g.MeasureString(graphicString, testFont);

                if (containerWidth > Convert.ToInt32(adjustedSizeNew.Width))
                {
                    // Good font, return it
                    return testFont;
                }
            }

            // If you get here there was no fontsize that worked
            // return minimumSize or original?
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
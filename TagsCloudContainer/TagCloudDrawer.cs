using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class TagCloudDrawer
    {
        public static Bitmap DrawTagCloud(List<WordLayout> wordLayouts, Color bgColor)
        {
            var wordBorders = wordLayouts.Select(l => l.RectangleBorder);
            var top = wordBorders.Min(b => b.Top);
            var bottom = wordBorders.Min(b => b.Bottom);
            var right = wordBorders.Min(b => b.Right);
            var left = wordBorders.Min(b => b.Left);

            var imageWidth = right - left;
            var imageHeight = bottom - top;

            var bitmap = new Bitmap(imageWidth, imageHeight);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(new SolidBrush(bgColor), 0, 0, imageWidth, imageHeight);
                foreach (var wordLayout in wordLayouts)
                {
                    graphics.DrawString(
                        wordLayout.Word,
                        wordLayout.Font,
                        new SolidBrush(wordLayout.Color),
                        wordLayout.RectangleBorder);
                }
            }

            return bitmap;
        }
    }
}

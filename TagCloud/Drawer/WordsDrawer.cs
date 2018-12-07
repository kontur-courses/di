using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Data;

namespace TagCloud.Drawer
{
    public class WordsDrawer : IWordsDrawer
    {
        private const int SideShift = 10;
        private const int MaximumSize = 4000;

        public Bitmap CreateImage(IEnumerable<WordImageInfo> infos, Brush wordsBrush, Brush backgroundBrush)
        {
            var imageInfos = infos as WordImageInfo[] ?? infos.ToArray();

            var right = imageInfos.Max(info => info.Rectangle.Right);
            var left = imageInfos.Min(info => info.Rectangle.Left);
            var bottom = imageInfos.Max(info => info.Rectangle.Bottom);
            var top = imageInfos.Min(info => info.Rectangle.Top);

            var width = Math.Min(right - left + SideShift, MaximumSize);
            var height = Math.Min(bottom - top + SideShift, MaximumSize);

            var image = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.FillRectangle(backgroundBrush, new Rectangle(0, 0, image.Width, image.Height));
                graphics.TranslateTransform(-left + SideShift / 2f, -top + SideShift / 2f);
                foreach (var info in imageInfos)
                    graphics.DrawString(info.Word, info.Font, wordsBrush, info.Rectangle);
            }

            return image;
        }
    }
}
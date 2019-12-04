using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Layouters;

namespace TagsCloud.Decorators
{
    public class ColoredDecorator : SimpleDecorator
    {
        public Color BackgroundColor { get; set; } = Color.LightGray;
        public Color TagColor { get; set; } = Color.LightGray;
        public Color TagRectangleColor { get; set; } = Color.LightGray;
        public Color TagTextColor { get; set; } = Color.Red;

        private float FontSizeByTagRate(int minRate, int maxRate, int rate)
        {
            if (rate <= minRate) return MinFontSize;
            if (rate >= maxRate) return MaxFontSize;
            return MinFontSize + (MaxFontSize - MinFontSize) * (rate - minRate) / (maxRate - minRate);
        }

        public override void Render(List<LayoutItem> layoutItems, Image image)
        {
            if (layoutItems.Count() == 0)
                throw new ArgumentException("There are no items.");

            var minRate = layoutItems.Min(item => item.Rate);
            var maxRate = layoutItems.Max(item => item.Rate);

            var left = layoutItems.Min(item => item.Rectangle.Left);
            var right = layoutItems.Max(item => item.Rectangle.Right);
            var top = layoutItems.Min(item => item.Rectangle.Top);
            var bottom = layoutItems.Max(item => item.Rectangle.Bottom);

            var backgroundBrush = new SolidBrush(BackgroundColor);
            var tagBrush = new SolidBrush(TagColor);
            var tagRectanglePen = new Pen(TagRectangleColor);
            var tagTextBrush = new SolidBrush(TagTextColor);

            using (var bmp = new Bitmap(right - left + 1, bottom - top + 1))
            {
                using (var graphics = Graphics.FromImage(bmp))
                {
                    graphics.FillRectangle(backgroundBrush, 0, 0, bmp.Width, bmp.Height);
                    foreach (var item in layoutItems)
                    {
                        graphics.FillRectangle(tagBrush, item.Rectangle.X - left, item.Rectangle.Y - top, item.Rectangle.Width, item.Rectangle.Height);
                        graphics.DrawRectangle(tagRectanglePen, item.Rectangle.X - left, item.Rectangle.Y - top, item.Rectangle.Width, item.Rectangle.Height);
                        var font = new Font(TagFont.Name, FontSizeByTagRate(minRate, maxRate, item.Rate), GraphicsUnit.Pixel);
                        graphics.DrawString(item.Text, font, tagTextBrush, item.Rectangle.X - left, item.Rectangle.Y - top - 1);
                    }
                }

                using (var graphics = Graphics.FromImage(image))
                {
                    graphics.DrawImage(bmp, new Rectangle(0, 0, image.Width, image.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel);
                }
            }
        }
    }
}

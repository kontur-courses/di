using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Layouters;

namespace TagsCloud.Renderers
{
    public class SimpleRenderer : ITagsCloudRenderer
    {
        public Font TagFont { get; set; } = SystemFonts.DefaultFont;
        public int MinFontSize { get; set; } = 30;
        public int MaxFontSize { get; set; } = 100;

        public virtual void CalcTagsRectanglesSizes(List<LayoutItem> layoutItems)
        {
            var minRate = layoutItems.Min(item => item.Rate);
            var maxRate = layoutItems.Max(item => item.Rate);
            using (Graphics g = Graphics.FromImage(new Bitmap(10, 10)))
            {
                foreach (var item in layoutItems)
                {
                    var font = new Font(SystemFonts.DefaultFont.FontFamily, 
                        FontSizeByTagRate(minRate, maxRate, item.Rate), GraphicsUnit.Pixel);
                    Size size = g.MeasureString(item.Text, font).ToSize();
                    item.Rectangle = new Rectangle(item.Rectangle.Location, size);
                }
            }
        }

        private float FontSizeByTagRate(int minRate, int maxRate, int rate)
        {
            if (rate <= minRate) return MinFontSize;
            if (rate >= maxRate) return MaxFontSize;
            return MinFontSize + (MaxFontSize - MinFontSize) * (rate - minRate) / (maxRate - minRate);
        }

        public virtual void Render(List<LayoutItem> layoutItems, Image image)
        {
            if (layoutItems.Count() == 0)
                throw new ArgumentException("There are no items.");

            var minRate = layoutItems.Min(item => item.Rate);
            var maxRate = layoutItems.Max(item => item.Rate);

            var left = layoutItems.Min(item => item.Rectangle.Left);
            var right = layoutItems.Max(item => item.Rectangle.Right);
            var top = layoutItems.Min(item => item.Rectangle.Top);
            var bottom = layoutItems.Max(item => item.Rectangle.Bottom);

            using (var bmp = new Bitmap(right - left + 1, bottom - top + 1))
            {
                using (var graphics = Graphics.FromImage(bmp))
                {
                    using (var background = new SolidBrush(Color.White))
                    {
                        graphics.FillRectangle(background, 0, 0, bmp.Width, bmp.Height); 
                    }

                    using (var tagColor = new SolidBrush(Color.LightGray))
                    {
                        using (var tagRectanglePen = new Pen(Color.Black))
                        {
                            using (var tagTextColor = new SolidBrush(Color.Black))
                            {
                                foreach (var item in layoutItems)
                                {
                                    graphics.FillRectangle(tagColor, item.Rectangle.X - left, item.Rectangle.Y - top, item.Rectangle.Width, item.Rectangle.Height);
                                    graphics.DrawRectangle(tagRectanglePen, item.Rectangle.X - left, item.Rectangle.Y - top, item.Rectangle.Width, item.Rectangle.Height);
                                    var font = new Font(TagFont.Name, FontSizeByTagRate(minRate, maxRate, item.Rate), GraphicsUnit.Pixel);
                                    graphics.DrawString(item.Text, font, tagTextColor, item.Rectangle.X - left, item.Rectangle.Y - top - 1);
                                }
                            }
                        }
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

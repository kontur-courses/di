using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Layouters;

namespace TagsCloud.Renderers
{
    public class SimpleRenderer : ITagsCloudRenderer
    {
        public int ImageWidth { get; set; } = 500;
        public int ImageHeight { get; set; } = 500;
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

        public virtual Image Render(List<LayoutItem> layoutItems)
        {
            if (layoutItems.Count() == 0)
                throw new ArgumentException("There are no items.");

            var minRate = layoutItems.Min(item => item.Rate);
            var maxRate = layoutItems.Max(item => item.Rate);

            var left = layoutItems.Min(item => item.Rectangle.Left);
            var right = layoutItems.Max(item => item.Rectangle.Right);
            var top = layoutItems.Min(item => item.Rectangle.Top);
            var bottom = layoutItems.Max(item => item.Rectangle.Bottom);

            var image = new Bitmap(ImageWidth, ImageHeight);
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

                CopyImage(bmp, image);
            }
            return image;
        }

        protected void CopyImage(Image src, Image dest)
        {
            using (var graphics = Graphics.FromImage(dest))
            {
                var srcAspectRatio = (double)src.Width / src.Height;
                var dstAspectRatio = (double)ImageWidth / ImageHeight;

                int destWidth, destHeight;
                if (srcAspectRatio > dstAspectRatio)
                {
                    destWidth = ImageWidth;
                    destHeight = (int)((double)destWidth / src.Width * src.Height);
                }
                else
                {
                    destHeight = ImageHeight;
                    destWidth = (int)((double)destHeight / src.Height * src.Width);
                }

                var x = destWidth < ImageWidth ? (ImageWidth - destWidth) / 2 : 0;
                var y = destHeight < ImageHeight ? (ImageHeight - destHeight) / 2 : 0;
                graphics.DrawImage(src, new Rectangle(x, y, destWidth, destHeight), 0, 0, src.Width, src.Height, GraphicsUnit.Pixel);
            }
        }
    }
}

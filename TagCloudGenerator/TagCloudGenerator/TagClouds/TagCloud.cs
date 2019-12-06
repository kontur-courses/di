using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using TagCloudGenerator.CloudLayouters;
using TagCloudGenerator.Tags;

namespace TagCloudGenerator.TagClouds
{
    public abstract class TagCloud
    {
        protected abstract Color BackgroundColor { get; }

        public Bitmap CreateBitmap(string[] cloudStrings, ICloudLayouter cloudLayouter, Size bitmapSize)
        {
            var bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            using var graphics = GetGraphics(bitmap);
            using var backgroundBrush = new SolidBrush(BackgroundColor);

            graphics.FillRectangle(backgroundBrush, new Rectangle(Point.Empty, bitmap.Size));

            var tagDrawer = GetTagDrawer(graphics);

            foreach (var tag in GetTags(cloudStrings, graphics, cloudLayouter))
                tagDrawer(tag);

            return bitmap;
        }

        protected abstract Action<Tag> GetTagDrawer(Graphics graphics);

        protected abstract IEnumerable<Tag> GetTags(string[] cloudStrings,
                                                    Graphics graphics,
                                                    ICloudLayouter circularCloudLayouter);

        protected static Brush GetBrush(Color color, Dictionary<Color, Brush> brushByColor)
        {
            if (brushByColor.TryGetValue(color, out var brush))
                return brush;
            return brushByColor[color] = new SolidBrush(color);
        }

        private static Graphics GetGraphics(Image tagCloudImage)
        {
            var graphics = Graphics.FromImage(tagCloudImage);

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            return graphics;
        }
    }
}
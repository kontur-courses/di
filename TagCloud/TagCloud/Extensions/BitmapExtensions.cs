using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TagCloud.Extensions
{
    internal static class BitmapExtensions
    {
        public static Bitmap ScaledResize(this Bitmap bitmap, Size targetSize, Color backgroundColor)
        {
            var scale = Math.Min((double) targetSize.Width / bitmap.Width, (double) targetSize.Height / bitmap.Height);
            var scaleWidth = (int) (bitmap.Width * scale);
            var scaleHeight = (int) (bitmap.Height * scale);
            var destRect = new Rectangle((targetSize.Width - scaleWidth) / 2, (targetSize.Height - scaleHeight) / 2,
                scaleWidth, scaleHeight);
            var scaledImage = new Bitmap(targetSize.Width, targetSize.Height);

            scaledImage.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);

            using var g = Graphics.FromImage(scaledImage);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.Clear(backgroundColor);

            using var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            g.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, wrapMode);

            return scaledImage;
        }
    }
}
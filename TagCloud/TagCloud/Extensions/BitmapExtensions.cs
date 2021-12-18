using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace TagCloud.Extensions
{
    internal static class BitmapExtensions
    {
        private static int _count;

        public static void SaveWithPostfix(this Bitmap bitmap, string? filename = null,
            string? targetDirectory = null, ImageFormat? format = null)
        {
            targetDirectory ??= Directory.GetCurrentDirectory();
            var imagesDirectory = Directory.CreateDirectory(Path.Combine(targetDirectory, "images"));
            var name = filename != null
                ? $"{filename}_{_count++}"
                : $"tagCloud_{_count++}";
            var path = Path.Combine(imagesDirectory.FullName, name);

            format ??= ImageFormat.Png;
            var extension = format.ToString().ToLower();
            bitmap.Save($"{path}.{extension}", format);
        }

        public static Bitmap ScaledResize(this Bitmap bitmap, Size targetSize, Color backgroundColor)
        {
            var scale = Math.Min((double) targetSize.Width / bitmap.Width, (double) targetSize.Height / bitmap.Height);
            var scaleWidth = (int) (bitmap.Width * scale);
            var scaleHeight = (int) (bitmap.Height * scale);
            var destRect = new Rectangle((targetSize.Width - scaleWidth) / 2, (targetSize.Height - scaleHeight) / 2,
                scaleWidth, scaleHeight);
            var scaledImage = new Bitmap(targetSize.Width, targetSize.Height);

            scaledImage.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);

            using var graphics = Graphics.FromImage(scaledImage);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.Clear(backgroundColor);

            using var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, wrapMode);

            return scaledImage;
        }
    }
}
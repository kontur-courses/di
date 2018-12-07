using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TagCloud.Utility
{
    public static class Helper
    {
        public static Bitmap ResizeImage(Image image, string size)
        {
            var width = int.Parse(size.Split('x').First());
            var height = int.Parse(size.Split('x').Last());
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }

        public static void CheckPaths(Options options)
        {
            if (!Path.HasExtension(options.PathToWords))
                throw new ArgumentException(
                    $"Path to words should contain file type, but was {options.PathToWords}");
            if (!File.Exists(options.PathToWords))
                throw new ArgumentException(
                    $"File {options.PathToWords} doesn't exists!");

            if (!Path.HasExtension(options.PathToPicture))
                throw new ArgumentException(
                    $"Path to picture should contain picture type, but was {options.PathToPicture}");
            if (!File.Exists(options.PathToPicture))
                throw new ArgumentException(
                    $"File {options.PathToPicture} doesn't exists!");

            if (options.PathToTags != null)
            {
                if (!Path.HasExtension(options.PathToTags))
                    throw new ArgumentException(
                        $"Path to tags should contain file type, but was {options.PathToTags}");
                if (!File.Exists(options.PathToTags))
                    throw new ArgumentException(
                        $"File {options.PathToTags} doesn't exists!");
            }

            if (options.PathToStopWords != null)
            {
                if (!Path.HasExtension(options.PathToStopWords))
                    throw new ArgumentException(
                        $"Path to stopwords should contain file type, but was {options.PathToStopWords}");
                if (!File.Exists(options.PathToStopWords))
                    throw new ArgumentException(
                        $"File {options.PathToStopWords} doesn't exists!");
            }
        }

        public static string GetPath(string path)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), path);
        }

        public static ImageFormat GetImageFormat(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
                throw new ArgumentException(
                    $"Unable to determine file extension for {fileName}");

            switch (extension.ToLower())
            {
                case @".bmp":
                    return ImageFormat.Bmp;

                case @".gif":
                    return ImageFormat.Gif;

                case @".ico":
                    return ImageFormat.Icon;

                case @".jpg":
                case @".jpeg":
                    return ImageFormat.Jpeg;

                case @".png":
                    return ImageFormat.Png;

                case @".tif":
                case @".tiff":
                    return ImageFormat.Tiff;

                case @".wmf":
                    return ImageFormat.Wmf;

                default:
                    throw new ArgumentException(
                        $"Unable to determine picture extension for file: {fileName}");
            }
        }
    }
}
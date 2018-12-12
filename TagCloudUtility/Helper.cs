using System;
using System.Drawing.Imaging;
using System.IO;
using TagCloud.Utility.Container;

namespace TagCloud.Utility
{
    public static class Helper
    {
        public static string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }

        public static void CheckPaths(Options options)
        {
            if (!File.Exists(options.PathToWords))
                throw new ArgumentException(
                    $"File {options.PathToWords} doesn't exists!");
            if (!Path.HasExtension(options.PathToWords))
                throw new ArgumentException(
                    $"Path to words should contain file type, but was {options.PathToWords}");

            if (!Path.HasExtension(options.PathToPicture))
                throw new ArgumentException(
                    $"Path to picture should contain picture type, but was {options.PathToPicture}");


            if (options.PathToTags != null)
            {
                if (!File.Exists(options.PathToTags))
                    throw new ArgumentException(
                        $"File {options.PathToTags} doesn't exists!");
                if (!Path.HasExtension(options.PathToTags))
                    throw new ArgumentException(
                        $"Path to tags should contain file type, but was {options.PathToTags}");
            }

            if (options.PathToStopWords != null)
            {
                if (!File.Exists(options.PathToStopWords))
                    throw new ArgumentException(
                        $"File {options.PathToStopWords} doesn't exists!");
                if (!Path.HasExtension(options.PathToStopWords))
                    throw new ArgumentException(
                        $"Path to stopwords should contain file type, but was {options.PathToStopWords}");
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
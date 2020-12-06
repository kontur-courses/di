using System;
using System.IO;
using System.Drawing;

namespace TagsCloud
{
    public static class ArgumentParser
    {
        public static string CheckFilePath(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException("File does not exist");

            if (!filePath.EndsWith(".txt") && !filePath.EndsWith("docx"))
                throw new ArgumentException("Unsupported file format");

            return filePath;
        }

        public static Color GetColor(string colorName)
        {
            try
            {
                return Color.FromName(colorName);
            }
            catch (Exception)
            {
                throw new ArgumentException("Unknown color");
            }
        }

        public static FontFamily GetFontFamily(string fontFamilyName)
        {
            try
            {
                return new FontFamily(fontFamilyName);
            }
            catch (Exception)
            {
                throw new ArgumentException("Unknown font family");
            }
        }

        public static bool IsCorrectFormat(string format)
        {
            if (format == "png" || format == "jpg" || format == "jpeg" || format == "bmp")
                return true;

            throw new ArgumentException("Unsupported file format");
        }

        public static Size GetSize(string size)
        {
            var widthAndHeight = size.Split('x');

            var result =
                int.TryParse(widthAndHeight[0], out var width) && int.TryParse(widthAndHeight[1], out var height)
                    ? new Size(width, height)
                    : throw new ArgumentException("The size is set incorrectly");

            return result;
        }
    }
}

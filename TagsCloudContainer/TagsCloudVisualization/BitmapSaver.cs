using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class BitmapSaver
    {
        public static void SaveBitmapToDirectory(Bitmap imageBitmap, string savePath)
        {
            if (!PathInRightFormat(savePath))
                throw new ArgumentException("wrong path format");

            using (imageBitmap)
            {
                imageBitmap.Save(savePath, ImageFormat.Png);
            }
        }

        private static bool PathInRightFormat(string path)
        {
            var pattern = @"((?:[^\\]*\\)*)(.*[.].+)";
            var match = Regex.Match(path, pattern);
            var directoryPath = match.Groups[1].ToString();

            return Directory.Exists(directoryPath) && match.Groups[2].Success;
        }
    }
}
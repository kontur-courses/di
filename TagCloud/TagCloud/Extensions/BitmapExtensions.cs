using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagCloud.Extensions
{
    internal static class BitmapExtensions
    {
        private static int _count;

        public static void SaveCurrentDirectory(this Bitmap bitmap, string? filename = null,
            string? saveDirectory = null, ImageFormat? format = null)
        {
            var targetDirectory = saveDirectory ?? AppDomain.CurrentDomain.BaseDirectory;
            var imagesDirectory = Directory.CreateDirectory(Path.Combine(targetDirectory, "images"));
            var name = filename ?? $"tag_cloud_{_count++}";
            var path = Path.Combine(imagesDirectory.FullName, name);
            
            var imageFormat = format ?? ImageFormat.Png;
            var extension = imageFormat.ToString().ToLower();
            
            bitmap.Save($"{path}.{extension}", imageFormat);
        }
    }
}
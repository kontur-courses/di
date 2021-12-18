using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagCloud.BitmapSaving
{
    internal static class TagCloudImageSaver
    {
        private static int _count;

        public static void Save(Bitmap bitmap, string? filename = null,
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
    }
}
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

namespace TagsCloudContainer.TagCloudVisualization
{
    public static class ImgSaver
    {
        public static string Save(this Bitmap bitmap, string strImageFormat)
        {
            var imageFormat = ParseImageFormat(strImageFormat);
            var path = Directory.GetCurrentDirectory();
            var fullname = $"{path}\\tagCloud.jpeg";
            bitmap.Save(fullname, imageFormat);
            return fullname;
        }

        private static ImageFormat ParseImageFormat(string strImageFormat)
        {
            return (ImageFormat)typeof(ImageFormat)
                .GetProperty(strImageFormat, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase)
                ?.GetValue(null);
        } 
    }
}
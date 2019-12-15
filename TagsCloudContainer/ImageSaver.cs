using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class ImageSaver : IFileSaver
    {
        private ImageFormat format;

        public ImageSaver(string format)
        {
            this.format = ParseImageFormat(format);
        }

        public static ImageFormat ParseImageFormat(string str)
        {
            return (ImageFormat) typeof(ImageFormat)
                .GetProperty(str, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase)
                .GetValue(null);
        }

        public void Save(Bitmap bitmap, string path)
        {
            bitmap.Save(path, format);
        }
    }
}
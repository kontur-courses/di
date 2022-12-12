using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer
{
    public static class BitmapExtension
    {
        public static void Save(this Bitmap bitmap, string path, string filename, ImageFormat format)
        {
            bitmap.Save($"{path}\\{filename}.{format}", format);
        }
    }
}
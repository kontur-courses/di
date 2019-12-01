using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudLibrary.Writers
{
    public class PngWriter : IImageWriter
    {
        public void WriteBitmapToFile(Bitmap bitmap, string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if (extension == "")
            {
                fileName = fileName + ".png";
            }
            bitmap.Save(fileName, ImageFormat.Png);
        }
    }
}
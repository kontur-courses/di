using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudLibrary.Writers
{
    public class BmpWriter : IImageWriter
    {
        public void WriteBitmapToFile(Bitmap bitmap, string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if (extension == "")
            {
                fileName = fileName + ".bmp";
            }
            bitmap.Save(fileName, ImageFormat.Bmp);
        }
    }
}

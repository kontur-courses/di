using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudLibrary.Writers
{
    public class JpegWriter : IImageWriter
    {
        public void WriteBitmapToFile(Bitmap bitmap, string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if (extension == "")
            {
                fileName = fileName + ".jpeg";
            }
            bitmap.Save(fileName, ImageFormat.Jpeg);
        }
    }
}

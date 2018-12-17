using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Drawing
{
    public class FileWriter : IWriter
    {
        public void WriteToFile(Bitmap bitmap, string filename, ImageFormat format)
        {
            bitmap.Save(filename, format);
        }
    }
}
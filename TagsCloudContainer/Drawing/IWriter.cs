using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Drawing
{
    public interface IWriter
    {
        void WriteToFile(Bitmap bitmap, string filename, ImageFormat format);
    }
}
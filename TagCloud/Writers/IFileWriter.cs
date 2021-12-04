using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.Writers
{
    public interface IFileWriter
    {
        void Write(Bitmap bitmap, string filename, ImageFormat format);
    }
}

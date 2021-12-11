using System.Drawing;

namespace TagCloud.Writers
{
    public interface IFileWriter
    {
        void Write(Bitmap bitmap, string filename, string extension);
    }
}

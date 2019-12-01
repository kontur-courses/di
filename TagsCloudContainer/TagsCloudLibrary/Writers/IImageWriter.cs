using System.Drawing;

namespace TagsCloudLibrary.Writers
{
    public interface IImageWriter
    {
        void WriteBitmapToFile(Bitmap bitmap, string fileName);
    }
}
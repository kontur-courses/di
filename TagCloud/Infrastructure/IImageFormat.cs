using System.Drawing;

namespace TagCloud.Infrastructure
{
    public interface IImageFormat
    {
        void SaveImage(Bitmap bitmap, string filePath);
    }
}

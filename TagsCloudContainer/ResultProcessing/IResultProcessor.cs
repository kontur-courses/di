using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.ResultProcessing
{
    public interface IResultProcessor
    {
        void ProcessResult(Bitmap bitmap, string filePath, ImageFormat imageFormat);
    }
}
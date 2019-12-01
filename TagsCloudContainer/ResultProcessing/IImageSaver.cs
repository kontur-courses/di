using System.Drawing;

namespace TagsCloudContainer.ResultProcessing
{
    public interface IImageSaver
    {
        void SaveBitmap(Bitmap bitmap);
    }
}
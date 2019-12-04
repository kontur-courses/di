using System.Drawing;

namespace TagsCloudContainer.ResultProcessing.ImageSaving
{
    public interface IImageSaver
    {
        void SaveBitmap(Bitmap bitmap);
    }
}
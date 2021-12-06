using System.Drawing;

namespace TagsCloudContainer.ImageSaver
{
    public interface IImageSaver
    {
        void Save(Bitmap bitmap);
    }
}
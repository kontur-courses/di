using System.Drawing;

namespace TagsCloudContainer.CloudVisualizers.ImageSaving
{
    public interface IImageSaver
    {
        void Save(Bitmap bitmap);
    }
}
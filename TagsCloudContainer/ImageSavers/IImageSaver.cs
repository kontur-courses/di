using System.Drawing;

namespace TagsCloudContainer.ImageSavers
{
    public interface IImageSaver
    {
        void SaveImage(Image image, string namePath);
    }
}
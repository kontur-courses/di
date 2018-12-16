using System.Drawing;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.ImageSavers
{
    public class ImageSaver : IImageSaver
    {
        private readonly IImageSettings imageSettings;

        public ImageSaver(IImageSettings settings)
        {
            imageSettings = settings;
        }

        public void SaveImage(Image image, string namePath)
        {
            image.Save(namePath, imageSettings.Format);
        }
    }
}
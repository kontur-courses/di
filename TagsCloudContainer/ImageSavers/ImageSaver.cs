using System;
using System.Drawing;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.ImageSavers
{
    public class ImageSaver : IImageSaver
    {
        private readonly ImageSettings imageSettings;

        public ImageSaver(ImageSettings settings)
        {
            imageSettings = settings;
        }

        public void SaveImage(Image image, string namePath)
        {
            image.Save(namePath, imageSettings.Format);
        }
    }
}
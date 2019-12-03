using System;
using System.Drawing;

namespace TagsCloudContainer.CloudVisualizers.ImageSaving
{
    public class ImageSaver : IImageSaver
    {
        private Func<ImageSaverSettings> settingsFactory;
        public ImageSaver(Func<ImageSaverSettings> settingsFactory)
        {
            this.settingsFactory = settingsFactory;
        }

        public void Save(Bitmap bitmap)
        {
            var settings = settingsFactory();
            bitmap.Save(settings.Path, settings.Format);
        }
    }
}
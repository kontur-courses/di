using System.Drawing;

namespace TagsCloudContainer.CloudVisualizers.ImageSaving
{
    public class ImageSaver
    {
        private ImageSaverSettings settings;
        public ImageSaver(ImageSaverSettings settings)
        {
            this.settings = settings;
        }

        public void Save(Bitmap bitmap)
        {
            bitmap.Save(settings.Path, settings.Format);
        }
    }
}
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.ImageSaver
{
    internal class ImageSaverFactory : IImageSaverFactory
    {
        public IImageSaver GetSaver(ImageExt ext)
        {
            return new JpgSaver();
        }
    }
}
using System;

namespace TagsCloudVisualization.ImageSaver
{
    internal class ImageSaverFactory : IImageSaverFactory
    {
        public IImageSaver GetSaver(ImageExt ext)
        {
            switch (ext)
            {
                case ImageExt.Jpg:
                    return new JpgSaver();
            }

            throw new ArgumentException($"Unexpected ImageExt {ext}");
        }
    }
}
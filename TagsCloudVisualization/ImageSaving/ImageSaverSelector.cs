using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.ImageSaving
{
    public class ImageSaverSelector
    {
        private readonly IEnumerable<IImageSaver> imageSavers;

        public ImageSaverSelector(IEnumerable<IImageSaver> imageSavers)
        {
            this.imageSavers = imageSavers;
        }

        public IImageSaver SelectImageSaver(string extensionName)
        {
            foreach (var imageSaver in imageSavers)
                if (imageSaver.SupportedTypes().Contains(extensionName))
                    return imageSaver;

            throw new ArgumentException("Image type is not supported");
        }
    }
}

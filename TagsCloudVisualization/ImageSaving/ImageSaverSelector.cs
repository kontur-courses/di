using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.ImageSaving
{
    public class ImageSaverSelector
    {
        private readonly IDictionary<string, IImageSaver> imageSaverByExtension;

        public ImageSaverSelector(IEnumerable<IImageSaver> imageSavers)
        {
            imageSaverByExtension = new Dictionary<string, IImageSaver>();

            foreach (var imageSaver in imageSavers)
                foreach (var extension in imageSaver.SupportedTypes())
                    imageSaverByExtension[extension] = imageSaver;
        }

        public IImageSaver SelectImageSaver(string imageFileName)
        {
            var extension = imageFileName.ExtractFileExtension();
            if (extension != null && imageSaverByExtension.ContainsKey(extension))
                return imageSaverByExtension[extension];

            throw new ArgumentException("Image type is not supported");
        }
    }
}

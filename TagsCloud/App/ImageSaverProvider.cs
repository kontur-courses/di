using System.Collections.Generic;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class ImageSaverProvider
    {
        private readonly IEnumerable<IImageSaver> imageSavers;

        public ImageSaverProvider(IEnumerable<IImageSaver> imageSavers)
        {
            this.imageSavers = imageSavers;
        }

        public IImageSaver GetImageSaver(string extension)
        {
            return imageSavers.FirstOrDefault(x => x.Extensions.Contains(extension))
                   ?? imageSavers.FirstOrDefault(x => x.Extensions.Contains(".png"));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.ImageProcessing.SaverImage.ImageSavers;

namespace TagsCloud.ImageProcessing.SaverImage.Factory
{
    public class ImageSaverFactory : IImageSaverFactory
    {
        private readonly IImageConfig imageConfig;
        private readonly Dictionary<string, Func<IImageSaver>> savers;

        public ImageSaverFactory(IImageConfig imageConfig)
        {
            this.imageConfig = imageConfig;
            savers = new Dictionary<string, Func<IImageSaver>>();
        }

        public IImageSaver Create() =>
                savers.FirstOrDefault(pair => imageConfig.Path.EndsWith(pair.Key)).Value();

        public IImageSaverFactory Register(string imageEntesion, Func<IImageSaver> creationFunc)
        {
            savers[imageEntesion] = creationFunc;
            return this;
        }
    }
}

using System.Linq;
using TagsCloud.Factory;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.ImageProcessing.SaverImage.ImageSavers;

namespace TagsCloud.ImageProcessing.SaverImage.Factory
{
    public class ImageSaverFactory : ServiceFactory<IImageSaver>
    {
        private readonly ImageConfig imageConfig;

        public ImageSaverFactory(ImageConfig imageConfig)
        {
            this.imageConfig = imageConfig;
        }

        public override IImageSaver Create() =>
            services.FirstOrDefault(pair => imageConfig.Path.EndsWith(pair.Key)).Value();
    }
}

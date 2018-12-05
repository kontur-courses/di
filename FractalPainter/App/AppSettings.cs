using FractalPainting.Infrastructure.Common;
using Ninject.Activation;

namespace FractalPainting.App
{
    public class AppSettings : Provider<ImageSettings>, IImageDirectoryProvider, IImageSettingsProvider
    {
        public string ImagesDirectory { get; set; }
        public ImageSettings ImageSettings { get; set; }
        protected override ImageSettings CreateInstance(IContext context)
        {
            return ImageSettings;
        }
    }
}
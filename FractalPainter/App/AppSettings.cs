using FractalPainting.Infrastructure.Common;

namespace FractalPainting.App
{
    public class AppSettings : IImageDirectoryProvider
    {
        public string ImagesDirectory { get; set; }
        public ImageSettings ImageSettings { get; set; }
    }
}
using TagsCloud.ClientGUI.Infrastructure;

namespace TagsCloud.ClientGUI
{
    public class AppSettings : IImageDirectoryProvider
    {
        public ImageSettings ImageSettings { get; set; }
        public string ImagesDirectory { get; set; }
    }
}
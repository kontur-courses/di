using TagsCloudForm.Common;

namespace TagsCloudForm
{
    public class AppSettings : IImageDirectoryProvider
    {
        public string ImagesDirectory { get; set; }
        public ImageSettings ImageSettings { get; set; }
    }
}
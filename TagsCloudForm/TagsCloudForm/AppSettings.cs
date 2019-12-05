using TagsCloudForm.Common;

namespace TagsCloudForm
{
    public class AppSettings : IImageDirectoryProvider, IImageSettingsProvider
    {
        public string ImagesDirectory { get; set; }
        public string BoringWordsFile { get; set; }
        public ImageSettings ImageSettings { get; set; }
    }
}
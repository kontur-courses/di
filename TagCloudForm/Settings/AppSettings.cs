namespace TagCloudForm.Settings
{
    public class AppSettings : IImageDirectoryProvider
    {
        public string ImagesDirectory { get; set; } = ".";
        public const string FormName = "Tag cloud";
    }
}
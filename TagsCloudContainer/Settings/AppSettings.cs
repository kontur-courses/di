namespace TagsCloudContainer.Settings
{
    public class AppSettings : IFilePathProvider, IImageDirectoryProvider
    {
        public string WordsFilePath { get; set; }
        public string ImagesDirectory { get; set; }

        public AppSettings()
        {
            CreateDefaultSettings();
        }

        private void CreateDefaultSettings()
        {
            ImagesDirectory = ".";
            WordsFilePath = "words.txt";
        }
    }
}
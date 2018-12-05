namespace TagsCloudContainer.Settings
{
    public class FileSettings
    {
        public FileSettings(string inputFileName)
        {
            Filename = inputFileName;
        }

        public string Filename { get; }
    }
}
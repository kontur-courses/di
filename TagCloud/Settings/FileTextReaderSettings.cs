namespace TagCloud.Settings
{
    public class FileTextReaderSettings
    {
        public string FilePath { get; }

        public FileTextReaderSettings(string filePath)
        {
            FilePath = filePath;
        }
    }
}
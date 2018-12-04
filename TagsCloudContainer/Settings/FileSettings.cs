namespace TagsCloudContainer.Settings
{
    public class FileSettings
    {
        public string Filename { get; }
        
        public FileSettings(string inputFileName)
        {
            Filename = inputFileName;
        }
    }
}
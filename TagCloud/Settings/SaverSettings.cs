namespace TagCloud.Settings
{
    public class SaverSettings
    {
        public string Path { get; }
        public string FileName { get; }

        public SaverSettings(string path, string fileName)
        {
            Path = path;
            FileName = fileName;
        }
    }
}
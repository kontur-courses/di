namespace TagCloud.Settings
{
    public class SaverSettings
    {
        public string Path { get; }
        public string FileName { get; }
        public string Extention { get; }

        public SaverSettings(string path, string fileName, string extention)
        {
            Path = path;
            FileName = fileName;
            Extention = extention;
        }
    }
}
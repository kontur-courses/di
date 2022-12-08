namespace TagCloud.App.CloudCreatorDriver.ImageSaver.FileTypes
{
    public class FileType : IFileType
    {
        public FileType(string path)
        {
            Path = path;
        }

        public string Path { get; set; }
    }
}
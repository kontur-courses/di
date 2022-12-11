namespace TagCloud.App.CloudCreatorDriver.ImageSaver.FileTypes;

public class FullFileName : IFullFileName
{
    public string Path { get; }
        
    public FullFileName(string path)
    {
        Path = path;
    }
}
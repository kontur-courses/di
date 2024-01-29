namespace TagsCloudVisualization.Settings;

public class ImageSaverSettings
{
    public string PathToSaveDirectory { get; }
    public string FileName { get; }
    public string FileFormat { get; }

    public ImageSaverSettings(string pathToSaveDirectory, string fileName, string fileFormat)
    {
        PathToSaveDirectory = pathToSaveDirectory;
        FileName = fileName;
        FileFormat = fileFormat;
    }
}
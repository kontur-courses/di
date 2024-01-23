namespace TagsCloudVisualization.Settings;

public class FileSettings
{
    public string PathToBoringWords { get; }
    public string PathToText { get; }
    public string PathToSaveDirectory { get; }
    public string FileName { get; }
    public string FileFormat { get; }

    public FileSettings(string pathToBoringWords,
        string pathToText, 
        string pathToSaveDirectory, 
        string fileName,
        string fileFormat)
    {
        PathToBoringWords = pathToBoringWords;
        PathToText = pathToText;
        PathToSaveDirectory = pathToSaveDirectory;
        FileName = fileName;
        FileFormat = fileFormat;
    }
}
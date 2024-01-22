namespace TagsCloudVisualization.Settings;

public class FileSettings
{
    public string PathToBoringWords { get; }
    public string PathToText { get; }
    public string PathToSaveDirectory { get; }
    public string FileName { get; }
    public string FileFormat { get; }

    public FileSettings(string pathToBoringWords = @"..\..\BoringWords.txt",
        string pathToText = @"..\..\Text.txt", 
        string pathToSaveDirectory = @"..\..\Images", 
        string fileName = "Image",
        string fileForms = ".png")
    {
        PathToBoringWords = pathToBoringWords;
        PathToText = pathToText;
        PathToSaveDirectory = pathToSaveDirectory;
        FileName = fileName;
        FileFormat = fileForms;
    }
}

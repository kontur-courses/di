namespace TagsCloudContainer.FileProviders;

public class FileReader: IFileReader
{
    public string ReadFile(string filePath)
    {
        return File.ReadAllText(filePath);
    }
}
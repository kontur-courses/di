namespace TagCloud.App.WordPreprocessorDriver.InputStream;

public class TxtEncoder : IFileEncoder
{
    private const string FileType = "txt";

    public string GetText(string fileName)
    {
        if (!IsCompatibleFileType(FileType))
            throw new Exception($"Expected {FileType} filetype, " +
                                $"but was found {fileName.Split('.').LastOrDefault() ?? string.Empty}");
        try
        {
            return File.ReadAllText(fileName);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Can not read words from file", e);
        }
    }

    public bool IsCompatibleFileType(string fileName)
    {
        return fileName.EndsWith(FileType);
    }

    public string GetExpectedFileType()
    {
        return FileType;
    }
}
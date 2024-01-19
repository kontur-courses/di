namespace TagsCloudContainer.WordProcessing.WordInput;


public class TxtFileWordParser : IWordProvider
{
    private readonly string _filePath;
    
    public TxtFileWordParser(string filePath)
    {
        _filePath = filePath;
    }
    
    public string[] Words => Parse();
    
    private string[] Parse()
    {
        string[] line;
        try
        {
            line = File.ReadAllLines(_filePath);
        }
        catch (Exception e)
        {
            throw new IOException($"Failed to read from file {_filePath} Most likely the file path is incorrect.", e);
        }

        return line;
    }
}
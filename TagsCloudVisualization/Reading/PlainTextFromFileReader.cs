namespace TagsCloudVisualization.Reading;

public class PlainTextFromFileReader : ITextReader
{
    private readonly string _pathToFile;

    public PlainTextFromFileReader(string pathToFile)
    {
        _pathToFile = pathToFile;
    }

    public string ReadText()
    {
        if (!File.Exists(_pathToFile))
            throw new FileNotFoundException($"File not exists {_pathToFile}");

        return File.ReadAllText(_pathToFile);
    }
}
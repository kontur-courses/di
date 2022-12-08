namespace TagsCloudVisualization.Reading;

public class PlainTextReader : ITextReader
{
    public string LoadTextFromFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"File not exists {path}");


        return File.ReadAllText(path);
    }
}
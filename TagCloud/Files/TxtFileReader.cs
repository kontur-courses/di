namespace TagCloud.Files;

public class TxtFileReader : IFileReader
{
    public string Extension => ".txt";

    public string ReadAll(string filename)
    {
        using var stream = new StreamReader(filename);
        var text = stream.ReadToEnd();
        return text;
    }
}
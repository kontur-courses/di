namespace TagsCloudVisualization.TextReaders;

public class TxtTextReader : TextReader
{
    public TxtTextReader(string path) : base(path)
    {
    }

    public override string GetText()
    {
        using var reader = new StreamReader(path);
        return reader.ReadToEnd();
    }
}
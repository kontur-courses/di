namespace TagsCloudVisualization.TextReaders;

public class TxtTextReader : TextReader
{
    public TxtTextReader(SourceSettings settings) : base(settings)
    {
    }

    public override string GetText()
    {
        using var reader = new StreamReader(Settings.Path);
        return reader.ReadToEnd();
    }
}
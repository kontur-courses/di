namespace TagsCloudVisualization.TextReaders;

public abstract class TextReader
{
    protected string path;

    protected TextReader(string path)
    {
        this.path = path;
    }

    public abstract string GetText();
}
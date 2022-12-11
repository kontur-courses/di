namespace TagsCloudVisualization.TextProviders;

public class TxtTextProvider : ITextProvider
{
    private readonly string path;

    public TxtTextProvider(string path)
    {
        this.path = path;
    }
    public IEnumerable<string> GetText()
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Such txt file not found");
        }

        return File.ReadLines(path);
    }
}
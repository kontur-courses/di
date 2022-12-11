using Xceed.Words.NET;

namespace TagsCloudVisualization.TextProviders;

public class DocTextProvider : ITextProvider
{
    private readonly string path;

    public DocTextProvider(string path)
    {
        this.path = path;
    }
    public IEnumerable<string> GetText()
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Such Doc file not found");
        }

        yield return DocX.Load(path).Text;
    }
}
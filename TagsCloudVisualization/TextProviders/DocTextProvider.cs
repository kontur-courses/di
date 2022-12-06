using TagsCloudVisualization.Abstractions;
using Xceed.Words.NET;

namespace TagsCloudVisualization.TextProviders;

public class DocTextProvider : ITextProvider
{
    public IEnumerable<string> GetText(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException();
        }

        yield return DocX.Load(path).Text;
    }
}
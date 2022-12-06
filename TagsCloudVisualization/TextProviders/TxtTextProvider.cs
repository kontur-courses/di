using TagsCloudVisualization.Abstractions;

namespace TagsCloudVisualization.TextProviders;

public class TxtTextProvider : ITextProvider
{
    public IEnumerable<string> GetText(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException();
        }

        return File.ReadLines(path);
    }
}
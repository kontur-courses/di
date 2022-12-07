namespace TagsCloudVisualization.Preprocessors;

public class LowerCasePreprocessor : IPreprocessor
{
    public IEnumerable<string> Process(IEnumerable<string> text)
    {
        return text.Select(w => w.ToLowerInvariant());
    }
}
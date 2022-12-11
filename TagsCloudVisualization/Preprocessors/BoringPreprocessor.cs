namespace TagsCloudVisualization.Preprocessors;

public class BoringPreprocessor : IPreprocessor
{
    private readonly HashSet<string> boringWords;

    public BoringPreprocessor(IReadOnlyCollection<string> boringWords)
    {
        this.boringWords = boringWords.ToHashSet();
    }
    public IEnumerable<string> Process(IEnumerable<string> text)
    {
        return text.Where(w => !boringWords.Contains(w));
    }
}
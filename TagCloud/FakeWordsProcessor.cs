using TagCloud.Abstractions;

namespace TagCloud;

public class FakeWordsProcessor : IWordsProcessor
{
    public IEnumerable<string> Process(IEnumerable<string> words)
    {
        return words;
    }
}
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

internal class TextStats : ITextStats
{
    private readonly Dictionary<string, int> statistics = new();

    public TextStats()
    {
    }

    public void UpdateWord(string word)
    {
        if (!statistics.ContainsKey(word))
            statistics[word] = 0;
        statistics[word]++;
    }

    public void SetCount(int count)
    {
        TotalWordCount = count;
    }

    public IReadOnlyDictionary<string, int> Statistics => statistics;

    public int TotalWordCount { get; private set; }
}
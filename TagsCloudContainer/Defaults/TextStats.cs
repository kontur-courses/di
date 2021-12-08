using TagsCloudContainer.Abstractions;

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
            statistics[word] = 1;
        else
            statistics[word]++;

        TotalWordCount++;
    }

    public IReadOnlyDictionary<string, int> Statistics => statistics;

    public int TotalWordCount { get; private set; }
}
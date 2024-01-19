using TagsCloudContainer.WordProcessing.WordInput;

namespace TagsCloudContainer.WordProcessing.WordGrouping;

public class DefaultWordGrouper : IWordGrouperProvider
{
    private readonly string[] _words;
    
    public DefaultWordGrouper(string[] words)
    {
        _words = words;
    }

    public Dictionary<string, int> GrouppedWords => GroupWords();
    
    private Dictionary<string, int> GroupWords()
    {
        var frequency = new Dictionary<string, int>();
        foreach (var word in _words)
        {
            frequency.TryAdd(word, 0);
            frequency[word]++;
        }

        return frequency;
    }
}
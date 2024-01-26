using TagCloud.Domain.Settings;
using TagCloud.Domain.WordProcessing.Interfaces;

namespace TagCloud.Domain.WordProcessing.Extractors;

public class ExcludeExtractor : IWordExtractor
{
    private readonly WordSettings wordSettings;
    
    public ExcludeExtractor(WordSettings wordSettings)
    {
        this.wordSettings = wordSettings;
    }
    
    public bool IsSuitable(string word)
    {
        return wordSettings.Excluded.All(excluded => !excluded.Equals(word, StringComparison.InvariantCulture));
    }
}
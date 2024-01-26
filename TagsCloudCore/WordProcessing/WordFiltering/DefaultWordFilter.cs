using TagsCloudCore.Utils;
using TagsCloudCore.WordProcessing.WordInput;

namespace TagsCloudCore.WordProcessing.WordFiltering;

public class DefaultWordFilter : IWordFilter
{
    private readonly HashSet<string> _wordsToExclude;
    
    public DefaultWordFilter(IWordProvider wordsToExclude)
    {
        _wordsToExclude = WordProcessingUtils.RemoveDuplicates(wordsToExclude.Words.Select(w => w.ToLower()));
    }

    public string[] FilterWords(string[] words)
        => words
            .Where(w => !_wordsToExclude.Contains(w))
            .ToArray();
}
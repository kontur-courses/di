using TagsCloudContainer.WordProcessing.WordInput;

namespace TagsCloudContainer.WordProcessing.WordFiltering;

public class DefaultWordFiler : IWordProvider
{
    private readonly string[] _wordsToExclude;
    private readonly string[] _words;

    public DefaultWordFiler(IWordProvider words, IWordProvider wordsToExclude)
    {
        _words = words.Words;
        _wordsToExclude = wordsToExclude.Words;
    }

    public string[] Words => FilterWords();
    
    private string[] FilterWords()
        => _words
            .Where(w => !_wordsToExclude.Contains(w))
            .Select(w => w.ToLower())
            .ToArray();
}
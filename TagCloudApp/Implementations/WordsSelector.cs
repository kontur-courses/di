using TagCloudApp.Abstractions;
using TagCloudApp.Domain;

namespace TagCloudApp.Implementations;

public class WordsSelector : IWordsSelector
{
    private readonly IWordsLoader _wordsLoader;

    public WordsSelector(IWordsLoader wordsLoader)
    {
        _wordsLoader = wordsLoader;
    }

    public IEnumerable<WordInfo> GetWords() =>
        _wordsLoader.LoadWords()
            .Select(w => w.ToLower())
            .GroupBy(w => w)
            .Select(group => new WordInfo(group.Key, group.Count()));
}
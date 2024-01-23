using DeepMorphy;

namespace TagsCloud;

public class WordAnalyzer
{
    private readonly List<string> ExcludedSpeeches;
    private readonly List<string> SelectedSpeeches;
    private readonly List<string> BoringWords;

    public WordAnalyzer(WordAnalyzerSettings settings)
    {
        ExcludedSpeeches = WordAnalyzerHelper.GetConvertedSpeeches(settings.ExcludedSpeeches);
        SelectedSpeeches = WordAnalyzerHelper.GetConvertedSpeeches(settings.SelectedSpeeches);
        BoringWords = settings.BoringWords;

    }
    
    private IEnumerable<string> GetFilteredWords(IEnumerable<string?> words)
    {
        var morphAnalyzer = new MorphAnalyzer(true);
        var morphInfos = morphAnalyzer.Parse(words);
        return morphInfos.Where(morphInfo => !BoringWords.Contains(morphInfo.BestTag.Lemma) && morphInfo.Tags.All(tag => 
                                             !ExcludedSpeeches.Contains(tag["чр"]) &&
                                             (SelectedSpeeches.Count == 0 ||
                                              SelectedSpeeches
                                                  .Contains(tag["чр"]))))
            .Select(info => info.BestTag.HasLemma ? info.BestTag.Lemma :  info.Text);
    }

    public IEnumerable<WordInfo> GetFrequencyList(IEnumerable<string?> words)
    {
        var parsedWords = new Dictionary<string, int>();
        foreach (var word in GetFilteredWords(words))
        {
            parsedWords.TryAdd(word, 0);
            parsedWords[word]++;
        }

        return parsedWords.Select(x => WordInfo.Create(x.Key, x.Value)).OrderByDescending(info => info.Count)
            .ThenByDescending(info => info.Word);
    }
}
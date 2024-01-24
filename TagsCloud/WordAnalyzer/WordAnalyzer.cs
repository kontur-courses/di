using DeepMorphy;

namespace TagsCloud.WordAnalyzer;

public class WordAnalyzer
{
    private readonly WordAnalyzerSettings Settings;

    public WordAnalyzer(WordAnalyzerSettings settings)
    {
        Settings = settings;
    }

    private IEnumerable<string> GetFilteredWords(IEnumerable<string?> words)
    {
        var excludedSpeeches = WordAnalyzerHelper.GetConvertedSpeeches(Settings.ExcludedSpeeches);
        var selectedSpeeches = WordAnalyzerHelper.GetConvertedSpeeches(Settings.SelectedSpeeches);
        var morphAnalyzer = new MorphAnalyzer(true);
        var morphInfos = morphAnalyzer.Parse(words);
        return morphInfos.Where(morphInfo => !Settings.BoringWords.Any(item => item.Equals(morphInfo.BestTag.Lemma, StringComparison.OrdinalIgnoreCase)) &&
                                             !excludedSpeeches.Contains(morphInfo.BestTag["чр"]) &&
                                             (selectedSpeeches.Count == 0 ||
                                              selectedSpeeches.Contains(morphInfo.BestTag["чр"])))
            .Select(info => info.BestTag.HasLemma ? info.BestTag.Lemma : info.Text);
    }

    public IEnumerable<WordInfo> GetFrequencyList(IEnumerable<string?> words)
    {
        var parsedWords = new Dictionary<string, int>();
        foreach (var word in GetFilteredWords(words))
        {
            parsedWords.TryAdd(word, 0);
            parsedWords[word]++;
        }

        return parsedWords.Select(x => new WordInfo(x.Key, x.Value)).OrderByDescending(info => info.Count)
            .ThenBy(info => info.Word);
    }
}
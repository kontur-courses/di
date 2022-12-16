using TagCloudPainter.Common;
using TagCloudPainter.Lemmaizers;

namespace TagCloudPainter.Preprocessors;

public class WordPreprocessor : IWordPreprocessor
{
    private readonly ILemmaizer _lemmaizer;
    private readonly ParseSettings parseSettings;

    public WordPreprocessor(IParseSettingsProvider parseSettingsProvider, ILemmaizer lemmaizer)
    {
        _lemmaizer = lemmaizer;
        parseSettings = parseSettingsProvider.ParseSettings;
    }

    public Dictionary<string, int> GetWordsCountDictionary(IEnumerable<string> words)
    {
        if (words == null || words.Count() == 0)
            throw new ArgumentNullException();

        var wordsCount = new Dictionary<string, int>();
        foreach (var word in words)
        {
            if (IsSkiped(word))
                continue;
            var key = _lemmaizer.GetLemma(word).ToLower();
            if (wordsCount.ContainsKey(key))
                wordsCount[key]++;
            else
                wordsCount[key] = 1;
        }

        return wordsCount;
    }

    private bool IsSkiped(string word)
    {
        if (parseSettings.IgnoredWords.Count > 0 && parseSettings.IgnoredWords.Contains(word))
            return true;

        var morpheme = _lemmaizer.GetMorph(word);

        if (parseSettings.IgnoredMorphemes.Contains(morpheme))
            return true;

        return false;
    }
}
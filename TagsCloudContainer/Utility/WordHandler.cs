namespace TagsCloudContainer.utility;

public static class WordHandler
{
    private static readonly Predicate<string> BoringWordsExcludeRule = w => w.Length > 3;

    public static IEnumerable<(string word, int count)> Preprocessing(
        IEnumerable<(string word, int count)> frequencyDict, bool excludeBoring = true,
        string? customExcludeFilename = null)
    {
        return Preprocessing(frequencyDict, excludeBoring, customExcludeFilename, null);
    }

    public static IEnumerable<(string word, int count)> Preprocessing(
        IEnumerable<(string word, int count)> frequencyDict, bool excludeBoring = true,
        Predicate<string>? customExcludeRule = null)
    {
        return Preprocessing(frequencyDict, excludeBoring, null, customExcludeRule);
    }

    public static IEnumerable<(string word, int count)> Preprocessing(
        IEnumerable<(string word, int count)> frequencyDict, bool excludeBoring = true,
        string? customExcludeFilename = null, Predicate<string>? customExcludeRule = null)
    {
        if (!excludeBoring)
            return frequencyDict.Select(kvp => (kvp.word.ToLower(), kvp.count));

        var boringDict = WordDataSet.CreateFrequencyDict(
            TextHandler.ReadText("boringWords.txt")
        ).Select(kvp => kvp.word.ToLower());

        if (customExcludeFilename != null)
        {
            boringDict = boringDict.Union(WordDataSet.CreateFrequencyDict(
                TextHandler.ReadText(customExcludeFilename)
            ).Select(kvp => kvp.word.ToLower()));
        }

        return frequencyDict.Select(kvp => (kvp.word.ToLower(), kvp.count))
            .Where(kvp => BoringWordsExcludeRule(kvp.Item1))
            .Where(kvp => customExcludeRule == null || customExcludeRule(kvp.Item1))
            .Where(kvp => !boringDict.Contains(kvp.Item1));
    }
}
namespace TagsCloudContainer.utility;

public static class WordHandler
{
    private static readonly Predicate<string> BoringWordsExcludeRule = w => w.Length > 3;
    
    public static IEnumerable<(string word, int count)> Preprocessing(
        IEnumerable<(string word, int count)> frequencyDict,
        bool excludeByDefaultFile, bool excludeByDefaultRule)
    {
        return Preprocessing(frequencyDict, excludeByDefaultFile, excludeByDefaultRule, null, null);
    }
    
    public static IEnumerable<(string word, int count)> Preprocessing(
        IEnumerable<(string word, int count)> frequencyDict,
        bool excludeByDefaultFile, bool excludeByDefaultRule,
        string? customExcludeFilename)
    {
        return Preprocessing(frequencyDict, excludeByDefaultFile, excludeByDefaultRule, customExcludeFilename, null);
    }

    public static IEnumerable<(string word, int count)> Preprocessing(
        IEnumerable<(string word, int count)> frequencyDict,
        bool excludeByDefaultFile, bool excludeByDefaultRule,
        Predicate<string>? customExcludeRule)
    {
        return Preprocessing(frequencyDict, excludeByDefaultFile, excludeByDefaultRule, null, customExcludeRule);
    }

    public static IEnumerable<(string word, int count)> Preprocessing(
        IEnumerable<(string word, int count)> frequencyDict, bool excludeByDefaultFile, bool excludeByDefaultRule,
        string? customExcludeFilename, Predicate<string>? customExcludeRule)
    {
        frequencyDict = frequencyDict.Select(kvp => (kvp.word.ToLower(), kvp.count));

        if (excludeByDefaultFile)
        {
            var defaultBoringDict = WordDataSet.CreateFrequencyDict(
                TextHandler.ReadText("boringWords.txt")
            ).Select(kvp => kvp.word.ToLower());
            frequencyDict = frequencyDict.Where(kvp => !defaultBoringDict.Contains(kvp.word));
        }

        if (excludeByDefaultRule)
            frequencyDict = frequencyDict.Where(kvp => BoringWordsExcludeRule(kvp.word));

        if (customExcludeFilename != null)
        {
            var defaultBoringDict = WordDataSet.CreateFrequencyDict(
                TextHandler.ReadText(customExcludeFilename)
            ).Select(kvp => kvp.word.ToLower());
            frequencyDict = frequencyDict.Where(kvp => !defaultBoringDict.Contains(kvp.word));
        }

        if (customExcludeRule != null)
            frequencyDict = frequencyDict.Where(kvp => customExcludeRule(kvp.word));

        return frequencyDict;
    }
}
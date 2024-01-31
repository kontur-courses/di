namespace TagsCloudContainer.utility;

public class WordHandler
{ 
    public IEnumerable<(string word, int count)> Preprocessing(IEnumerable<(string word, int count)> frequencyDict, 
        string? excludeWords = null, Predicate<string>? excludeRule = null)
    {
        frequencyDict = frequencyDict.Select(kvp => (kvp.word.ToLower(), kvp.count));

        if (excludeWords != null)
        {
            var boringDict = new WordDataSet()
                .CreateFrequencyDict(excludeWords)
                .Select(kvp => kvp.word.ToLower());
            frequencyDict = frequencyDict.Where(kvp => !boringDict.Contains(kvp.word));
        }

        if (excludeRule != null)
            frequencyDict = frequencyDict.Where(kvp => excludeRule(kvp.word));

        return frequencyDict;
    }
}
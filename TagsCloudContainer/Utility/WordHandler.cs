namespace TagsCloudContainer.utility;

public class WordHandler(ITextHandler? excludeSource = null, Predicate<string>? excludeRule = null)
{ 
    public IEnumerable<(string word, int count)> Preprocessing(IEnumerable<(string word, int count)> frequencyDict)
    {
        frequencyDict = frequencyDict.Select(kvp => (kvp.word.ToLower(), kvp.count));

        if (excludeSource != null)
        {
            var boringDict = new WordDataSet(excludeSource)
                .CreateFrequencyDict()
                .Select(kvp => kvp.word.ToLower());
            frequencyDict = frequencyDict.Where(kvp => !boringDict.Contains(kvp.word));
        }

        if (excludeRule != null)
            frequencyDict = frequencyDict.Where(kvp => excludeRule(kvp.word));

        return frequencyDict;
    }
}
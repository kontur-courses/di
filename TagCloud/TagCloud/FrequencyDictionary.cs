namespace TagCloud;

public class FrequencyDictionary
{
    public Dictionary<string, int> GetWordsFrequency(IEnumerable<string> text)
    {
        var dictionary = new Dictionary<string, int>();
        foreach (var word in text)
            if (!dictionary.ContainsKey(word))
                dictionary.Add(word, 1);
            else
                dictionary[word]++;

        return dictionary;
    }
}
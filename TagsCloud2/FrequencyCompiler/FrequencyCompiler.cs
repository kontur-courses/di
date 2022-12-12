namespace TagsCloud2.FrequencyCompiler;

public class FrequencyCompiler : IFrequencyCompiler
{
    private IFrequencyCompiler frequencyCompilerImplementation;

    public Dictionary<string, int> GetFrequencyOfWords(List<string> words)
    {
        var frequency = new Dictionary<string, int>();
        foreach (var word in words)
        {
            if (!frequency.ContainsKey(word))
            {
                frequency.Add(word, 0);
            }

            frequency[word] += 1;
        }
        return frequency;
    }

    public List<WordFrequency> GetFrequencyList(Dictionary<string, int> frequencyOfWords, int amount)
    {
        var frequencyList = frequencyOfWords.Select(
            item => new WordFrequency(item.Key, item.Value)).ToList();
        
        frequencyList.Sort((x, y) => x.Frequency - y.Frequency);
        frequencyList.Reverse();
        var result = new List<WordFrequency>();
        var realAmount = Math.Min(amount, frequencyList.Count);
        for (int i = 0; i < realAmount; i++)
        {
            result.Add(frequencyList[i]);
        }

        return result;
    }
}
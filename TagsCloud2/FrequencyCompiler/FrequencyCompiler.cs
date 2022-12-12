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
        var frequencyList = new List<WordFrequency>();
        foreach (var item in frequencyOfWords)
        {
            frequencyList.Add(new WordFrequency(item.Key, item.Value));
        }
        
        frequencyList.Sort((x, y) => x.Frequency - y.Frequency);
        frequencyList.Reverse();
        var result = new List<WordFrequency>();
        for (int i = 0; i < Math.Min(amount, frequencyList.Count); i++)
        {
            result.Add(frequencyList[i]);
        }

        return result;
    }
}
namespace TagsCloud2;

public class FrequencyCompiler : IFrequencyCompiler
{
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
}
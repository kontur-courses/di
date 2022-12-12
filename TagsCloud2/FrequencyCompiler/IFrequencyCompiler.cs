namespace TagsCloud2.FrequencyCompiler;

public interface IFrequencyCompiler
{
    public Dictionary<string, int> GetFrequencyOfWords(List<string> words, HashSet<string> excludingWords);
    public List<WordFrequency> GetFrequencyList(Dictionary<string, int> frequencyOfWords, int amount);
}
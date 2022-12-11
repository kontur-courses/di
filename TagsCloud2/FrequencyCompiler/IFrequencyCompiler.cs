namespace TagsCloud2;

public interface IFrequencyCompiler
{
    public Dictionary<string, int> GetFrequencyOfWords(List<string> words);
}
namespace TagCloudContainer;

public interface IWordConfig
{
    public IEnumerable<string> Validate(IEnumerable<string> lines);
    public List<Word> ShuffleWords(List<Word> words);
}
namespace TagCloud;

public interface IWordPreprocessor
{
    public IEnumerable<string> Process(IEnumerable<string> words);
}
namespace TagCloud;

public interface IWordPreprocessor
{
    public List<string> Process(List<string> words, List<string>? excludedWords);
}
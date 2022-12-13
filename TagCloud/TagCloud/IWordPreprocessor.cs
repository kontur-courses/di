namespace TagCloud;

public interface IWordPreprocessor
{
    public List<string> Process(List<string> words, IReadOnlyList<string>? excludedWords);
}
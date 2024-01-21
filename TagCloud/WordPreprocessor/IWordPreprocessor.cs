namespace TagCloud.WordPreprocessor;

public interface IWordPreprocessor
{
    IEnumerable<string> HandleWords(IEnumerable<string> words);
}
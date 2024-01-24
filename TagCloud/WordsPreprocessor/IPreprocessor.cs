namespace TagCloud.WordsPreprocessor;

public interface IPreprocessor
{
    IEnumerable<string> HandleWords(IEnumerable<string> words);
}
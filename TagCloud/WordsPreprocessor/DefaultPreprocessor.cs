namespace TagCloud.WordsPreprocessor;

public class DefaultPreprocessor : IPreprocessor
{
    public IEnumerable<string> HandleWords(IEnumerable<string> words)
    {
        return words.Select(word => word.ToLower());
    }
}
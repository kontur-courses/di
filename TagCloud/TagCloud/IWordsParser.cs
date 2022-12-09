namespace TagCloud;

public interface IWordsParser
{
    public IEnumerable<string> Parse(string text);
}
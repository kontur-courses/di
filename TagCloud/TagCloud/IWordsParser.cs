namespace TagCloud;

public interface IWordsParser
{
    public List<string> Parse(string text);
}
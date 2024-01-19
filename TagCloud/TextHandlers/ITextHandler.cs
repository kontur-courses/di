namespace TagCloud.TextHandlers;

public interface ITextHandler
{
    IEnumerable<(string word, int count)> Handle();
}
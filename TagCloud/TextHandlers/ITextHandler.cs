namespace TagCloud.TextHandlers;

public interface ITextHandler
{
    IEnumerable<(string word, int size)> Handle(Stream stream);
}
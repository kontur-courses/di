namespace TagCloud.TextHandlers;

public interface ITextHandler
{
    Dictionary<string, int> Handle();
}
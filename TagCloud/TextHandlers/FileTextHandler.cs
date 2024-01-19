namespace TagCloud.TextHandlers;

public class FileTextHandler : ITextHandler
{
    public IEnumerable<(string word, int size)> Handle(Stream stream)
    {
        throw new NotImplementedException();
    }
}
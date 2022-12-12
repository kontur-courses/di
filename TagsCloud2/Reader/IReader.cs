namespace TagsCloud2.Reader;

public interface IReader
{
    public List<string> ReadWordsFromFile(string pathToFile);
}
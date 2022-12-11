namespace TagsCloud2;

public interface IReader
{
    public List<string> ReadWordsFromFile(string pathToFile);
}
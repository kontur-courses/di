namespace TagsCloud2.Reader;

public interface IWordsReader
{
    public List<string> ReadWordsFromFile(string pathToFile);
}
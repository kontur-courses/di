namespace TagCloud;

public interface IWordsReader
{
    List<string> Get(string path);
}
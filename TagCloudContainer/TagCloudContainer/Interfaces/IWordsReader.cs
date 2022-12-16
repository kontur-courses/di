namespace TagCloudContainer;

public interface  IWordsReader
{
    public IEnumerable<Word> GetWordsFromFile(string fileName);
}
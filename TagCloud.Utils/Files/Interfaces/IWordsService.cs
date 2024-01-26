namespace TagCloud.Utils.Files.Interfaces;

public interface IWordsService
{
    public IEnumerable<string> GetWords(string path);
}
namespace TagsCloudVisualization;

public interface IWordParser
{
    IEnumerable<string> GetAllWords(string path);
    IEnumerable<string> RemoveDullWords(IEnumerable<string> words, Func<string, bool> dullWordChecker);
}
namespace TagsCloudVisualization;

public interface IInterestingWordsParser
{
    IEnumerable<string> GetInterestingWords(string path);
}
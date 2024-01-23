namespace TagsCloudVisualization;

public interface IWordParser
{
    IEnumerable<string> GetInterestingWords(string path, IDullWordChecker dullWordChecker);
}
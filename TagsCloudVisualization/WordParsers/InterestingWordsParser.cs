using System.Text.RegularExpressions;

namespace TagsCloudVisualization;

public class InterestingWordsParser : IInterestingWordsParser
{
    private IDullWordChecker dullWordChecker;

    public InterestingWordsParser(IDullWordChecker dullWordChecker)
    {
        this.dullWordChecker = dullWordChecker;
    }
    
    public IEnumerable<string> GetInterestingWords(string path)
    {
        var readText = File.ReadAllText(path);
        var removedPunctuation = Regex.Replace(readText, @"[^\w\d\s]+", "");
        var words = Regex.Split(removedPunctuation, @"\s+")
            .Select(word => word.ToLower())
            .Where(word => !dullWordChecker.Check(word));
        return words;
    }
}
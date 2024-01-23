using System.Text.RegularExpressions;

namespace TagsCloudVisualization;

public class WordParser : IWordParser
{
    public IEnumerable<string> GetInterestingWords(string path, IDullWordChecker dullWordChecker)
    {
        var readText = File.ReadAllText(path);
        var removedPunctuation = Regex.Replace(readText, @"[^\w\d\s]+", "");
        var words = Regex.Split(removedPunctuation, @"\s+")
            .Select(word => word.ToLower())
            .Where(word => !dullWordChecker.Check(word));
        return words;
    }
}
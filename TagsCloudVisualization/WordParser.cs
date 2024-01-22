using System.Text.RegularExpressions;
using WeCantSpell.Hunspell;

namespace TagsCloudVisualization;

public class WordParser : IWordParser
{
    public IEnumerable<string> GetAllWords(string path)
    {
        var readText = File.ReadAllText(path);
        var removedPunctuation = Regex.Replace(readText, @"[^\w\d\s]+", "");
        var words = Regex.Split(removedPunctuation, @"\s+").Select(word => word.ToLower());
        return words;
    }
    
    public IEnumerable<string> RemoveDullWords(IEnumerable<string> words, Func<string, bool> dullWordChecker)
    {
        return words.Where(dullWordChecker);
    }
}
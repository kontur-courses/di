using System.Text.RegularExpressions;

namespace TagsCloudVisualization.Extensions;

public static class StringExtensions
{
    public static IEnumerable<string> GetAllWords(this string text)
    {
        var wordPattern = new Regex(@"[\w\d]+");
        return wordPattern
            .Matches(text)
            .Select(x => x.Value);
    }
}
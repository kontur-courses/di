using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagCloud
{
    public class TextParser : ITextParser
    {
        public IReadOnlyList<string> GetWords(string text)
        {
            var words = new List<string>();

            Regex regex = new Regex(@"\w+");

            MatchCollection matches = regex.Matches(text);

            foreach (Match match in matches)
                words.Add(match.Value);

            return words;

           // return text.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
        }


    }
}
    
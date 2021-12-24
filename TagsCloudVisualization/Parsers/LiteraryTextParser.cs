using System.Collections.Generic;
using System.Text;

namespace TagsCloudVisualization.Parsers
{
    internal class LiteraryTextParser : ITextParser
    {
        public IEnumerable<string> ParseText(string text)
        {
            var word = new StringBuilder();
            
            foreach (var symbol in text)
            {
                if (char.IsLetter(symbol) || symbol == '-') word.Append(symbol);
                else
                {
                    if (word.Length == 0) continue;
                    yield return word.ToString();
                    word.Clear();
                }
            }
            
            if (word.Length != 0) yield return word.ToString();
        }
    }
}
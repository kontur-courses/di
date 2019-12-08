using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudVisualization.TextPreprocessing
{
    public class WordsExtractor
    {
        public IEnumerable<string> GetWords(string text)
        {
            var words = Regex.Split(text, @"[^\w-]");
            return words.Select(word => Regex.Replace(word, @"[^\w-]", string.Empty))
                .Where(preparedWord => preparedWord != string.Empty && preparedWord != "-");
        }
    }
}
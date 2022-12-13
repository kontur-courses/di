using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TagsCloudVisualization.TextFormatters
{
    public class TextFormatter : ITextFormatter
    {
        public IWordFilter Filter;

        public TextFormatter(IWordFilter filter)
        {
            Filter = filter;
        }

        public List<Word> Format(string text)
        {
            var words = GetWords(text);
            words.ForEach(t => t.ToLower());
            words = words.Where(t => Filter.IsPermitted(t)).ToList();
            return GetAllWordsFromText(words).ToList();
        }

        private List<string> GetWords(string text)
        {
            var words = new List<string>();

            var regex = new Regex(@"\w+");

            var matches = regex.Matches(text);

            foreach (Match match in matches)
                words.Add(match.Value);

            return words;
        }

        private IEnumerable<Word> GetAllWordsFromText(IEnumerable<string> words)
        {
            var wordsCount = words.Count();

            return words.GroupBy(word => word)
                .OrderByDescending(group => group.Count()).Select(group =>
                {
                    var word = new Word(group.Key)
                    {
                        Frequency = (float)group.Count() / wordsCount
                    };
                    return word;
                });
        }
    }
}

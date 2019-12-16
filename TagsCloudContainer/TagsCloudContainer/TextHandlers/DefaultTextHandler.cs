using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloudContainer
{
    public class DefaultTextHandler : TextHandler
    {
        private static readonly Regex wordPattern = new Regex(@"\b[a-zA-Z]+", RegexOptions.Compiled);

        public DefaultTextHandler(ITextReader textReader, IDullWordsEliminator dullWordsEliminator) :
            base(textReader, dullWordsEliminator)
        {}

        public override Dictionary<string, int> GetWordsFrequencyDict()
        {
            var result = new Dictionary<string, int>();
            foreach (var line in textReader.GetLines())
            {
                foreach (Match match in wordPattern.Matches(line))
                {
                    var currentWord = match.Value.ToLower();
                    if (!dullWordsEliminator.IsDull(currentWord))
                        result[currentWord] = result.ContainsKey(currentWord) ? result[currentWord] + 1 : 1;
                }
            }
            return result;
        }
    }
}
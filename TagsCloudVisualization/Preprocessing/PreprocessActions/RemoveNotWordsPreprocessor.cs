using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloudVisualization.Preprocessing
{
    public class RemoveNotWordsPreprocessor : IPreprocessor
    {
        public IEnumerable<string> ProcessWords(IEnumerable<string> words)
        {
            var wordRegex = new Regex(@"^[a-zA-Z]+'?-?[a-zA-Z]+");
            foreach (var word in words)
                if (wordRegex.Match(word).Success)
                    yield return word;
        }
    }
}
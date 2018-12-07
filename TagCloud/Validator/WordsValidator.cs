using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Validator
{
    public class WordsValidator : IWordsValidator
    {
        public IEnumerable<string> Validate(IEnumerable<string> words, IEnumerable<string> boringWords)
        {
            var boringWordsSet = new HashSet<string>(boringWords);
            return words.Where(word => !boringWordsSet.Contains(word));
        }
    }
}
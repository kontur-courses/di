using System.Collections.Generic;
using Autofac.Features.AttributeFilters;

namespace TagsCloudVisualization.WordValidators
{
    public class IgnoredWordsValidator : IWordValidator
    {
        private readonly HashSet<string> boringWords;
        
        public IgnoredWordsValidator([KeyFilter("IgnoreWords")] IEnumerable<string> invalidWords)
        {
            boringWords = new HashSet<string>(invalidWords);
        }

        public bool Validate(string word)
        {
            return !boringWords.Contains(word);
        }
    }
}
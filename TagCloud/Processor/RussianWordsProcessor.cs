using System.Collections.Generic;
using System.Linq;
using NHunspell;

namespace TagCloud.Processor
{
    public class RussianWordsProcessor : IWordsProcessor
    {
        private readonly HashSet<string> boringWords;

        public RussianWordsProcessor(IEnumerable<string> boringWords)
        {
            this.boringWords = new HashSet<string>(boringWords);
        }

        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            var validatedWords = words
                .Select(word => word.ToLower())
                .Where(word => !boringWords.Contains(word));
            using (var hunspell = new Hunspell(@"..\..\Dictionaries\ru.aff", @"..\..\Dictionaries\ru.dic"))
            {
                foreach (var word in validatedWords)
                {
                    var stems = hunspell.Stem(word);
                    yield return stems.Count > 0 ? stems[0] : word;
                }
            }
        }
    }
}
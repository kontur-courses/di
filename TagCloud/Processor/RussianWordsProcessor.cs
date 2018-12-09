using System.Collections.Generic;
using System.Linq;
using NHunspell;

namespace TagCloud.Processor
{
    public class RussianWordsProcessor : IWordsProcessor
    {
        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            using (var hunspell = new Hunspell("ru.aff", "ru.dic"))
            {
                foreach (var word in words.Select(word => word.ToLower()))
                {
                    var stems = hunspell.Stem(word);
                    yield return stems.Count > 0 ? stems[0] : word;
                }
            }
        }
    }
}
using System.Collections.Generic;
using NHunspell;

namespace TagCloud.Processor
{
    public class StemWordsProcessor : IWordsProcessor
    {
        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            using (var hunspell = new Hunspell(@"..\..\Dictionaries\ru.aff", @"..\..\Dictionaries\ru.dic"))
            {
                foreach (var word in words)
                {
                    var stems = hunspell.Stem(word);
                    yield return stems.Count > 0 ? stems[0] : word;
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using NHunspell;

namespace TagsCloudContainer.Data.Processors
{
    public class WordStemProcessor : IWordProcessor
    {
        private readonly Func<Hunspell> hunspellFactory;
        
        public WordStemProcessor(Func<Hunspell> hunspellFactory)
        {
            this.hunspellFactory = hunspellFactory;
        }
        
        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            using (var hunspell = hunspellFactory())
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
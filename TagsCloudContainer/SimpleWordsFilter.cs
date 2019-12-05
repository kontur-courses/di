using System;
using System.Collections.Generic;
using System.Linq;
using NHunspell;

namespace TagsCloudContainer
{
    public class SimpleWordsFilter : IWordsFilter
    {
        private readonly string[] Words;
        
        public SimpleWordsFilter(string[] arr)
        {
            Words = arr;
        }

        public IEnumerable<string> FilterWords()
        {
            //Hunspell hunspell = new Hunspell("en_us.aff", "en_us.dic");
            return Words.Select(x=>x.ToLower());
                //.Where(x=> hunspell.Spell(x));
        }
    }
}
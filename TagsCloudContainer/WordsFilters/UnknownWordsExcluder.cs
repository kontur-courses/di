using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms.PropertyGridInternal;
using NHunspell;
using TagsCloudContainer.Dictionaries;

namespace TagsCloudContainer.WordsFilters
{
    public class NounsExcluder : IFilter<string>
    {
        private readonly IGrammarDictionary grammarDictionary;

        public NounsExcluder(IGrammarDictionary grammarDictionary)
        {
            this.grammarDictionary = grammarDictionary;
        }

        public bool IsCorrect(string value)
            => grammarDictionary.ContainsWord(value);
    }
}


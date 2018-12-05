using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TagCloud
{
    internal class SimpleWordsPreparer : IWordsPreparer
    {
        private readonly HashSet<string> boringWords;

        public IEnumerable<string> PrepareWords(IEnumerable<string> words)
        {
            return words.Where(w => !this.boringWords.Contains(w));
        }

        public SimpleWordsPreparer(string[] boringWords)
        {
            this.boringWords = boringWords.ToHashSet();
        }
    }

    
}
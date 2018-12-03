using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TagCloud
{
    internal class SimpleWordsPreparer : IWordsPreparer
    {
        private readonly HashSet<string> _boringWords;

        public IEnumerable<string> PrepareWords(IEnumerable<string> words)
        {
            return words.Where(w => !_boringWords.Contains(w));
        }

        public SimpleWordsPreparer(string[] boringWords)
        {
            _boringWords = boringWords.ToHashSet();
        }
    }

    
}
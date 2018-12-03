using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace TagCloud
{
    internal class SimpleWordsPreparer : IWordsPreparer
    {
        public IEnumerable<string> PrepareWords(IEnumerable<string> words) => throw new NotImplementedException();
    }

    
}
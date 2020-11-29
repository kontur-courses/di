using System;
using System.Collections.Generic;
using System.Text;

namespace TagCloud
{
    internal class WordsExcluder : IWordExcluder
    {
        public bool MustBeExclude(string word) => false;
    }
}

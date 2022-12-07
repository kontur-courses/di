using System;
using System.Collections.Generic;
using System.Text;

namespace TagCloud.BoringWordsStorage
{
    public interface IBoringWordsStorage
    {
        public HashSet<string> GetBoringWords();
    }
}

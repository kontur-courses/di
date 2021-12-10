using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2.Text
{
    public class ShortWordsSelector : ISillyWordSelector
    {
        public bool IsWordSilly(string word)
        {
            return word.Length <= 3;
        }
    }
}

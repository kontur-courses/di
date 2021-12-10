using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2.Text
{
    public class SillyWordsRemover : ISillyWordRemover
    {
        public string RemoveSillyWords(string input, ISillyWordSelector selector)
        {
            if (selector.IsWordSilly(input))
            {
                return "";
            }
            return input;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextConfiguration.WordProcessors
{
    public class ToLowerCaseProcessor : IWordProcessor
    {
        public string ProcessWord(string word)
        {
            return word.ToLower();
        }
    }
}

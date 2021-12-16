using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public class LinesWordReader : IWordReader
    {
        public string[] GetUniqueLowercaseWords(string input)
        {
            return input
                .ToLower()
                .Split('\r')
                .Distinct()
                .ToArray();
        }
    }
}

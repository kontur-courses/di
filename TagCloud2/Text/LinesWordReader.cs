using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public class LinesWordReader : IWordReader
    {
        public string[] GetWords(string input)
        {
            return input.Split('\r');
        }
    }
}

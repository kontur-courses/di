using System;
using System.Linq;

namespace TagCloud2
{
    public class LinesWordReader : IWordReader
    {
        public string[] GetUniqueLowercaseWords(string input)
        {
            return input
                .ToLower()
                .Split(Environment.NewLine)
                .Distinct()
                .ToArray();
        }
    }
}

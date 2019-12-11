using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
    public class UpperCaseParser : IParser
    {
        public bool IsChecked { get; set; }

        public string Name { get; private set; }

        public UpperCaseParser()
        {
            IsChecked = true;
            Name = "UpperCase parser";
        }

        public string[] ParseWords(string[] words) => words
            .Select(word => word.ToUpper())
            .ToArray();
    }
}

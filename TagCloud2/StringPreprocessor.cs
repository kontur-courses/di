using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public class StringPreprocessor : IStringPreprocessor
    {
        public string PreprocessString(string input)
        {
            if (input.Length > 3)
            {
                return input.ToLower();
            }

            return null;
        }
    }
}

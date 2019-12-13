using System;
using System.Collections.Generic;
using TagsCloud.Interfaces;
using System.Linq;

namespace TagsCloud.Spliters
{
    public class SpliterByWhiteSpace : ITextSpliter
    {
        private readonly char[] splitChar = { ',', '.', '!', '?', ';', ':', ' '};

        public IEnumerable<string> SplitText(string text)
        {
            if (text == null)
                throw new ArgumentNullException();
            return text.Split(Environment.NewLine).SelectMany(line => line.Split(splitChar)).Where(word => !string.IsNullOrEmpty(word));
        }
    }
}

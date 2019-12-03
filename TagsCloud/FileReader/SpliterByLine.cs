using System;
using System.Collections.Generic;
using TagsCloud.Interfaces;

namespace TagsCloud.FileReader
{
    public class SpliterByLine : ITextSpliter
    {
        private string splitChar = Environment.NewLine;

        public IEnumerable<string> SplitText(string text)
        {
            foreach (var word in text.Split(splitChar))
                yield return word;
        }
    }
}

using System;
using System.Collections.Generic;
using TagsCloud.Interfaces;

namespace TagsCloud.FileReader
{
    public class SpliterByWhiteSpace : ITextSpliter
    {
        private readonly char[] splitChar = { ',', '.', '!', '?', ';', ':', ' ' };

        public IEnumerable<string> SplitText(string text)
        {
            var words = new List<string>();
            foreach (var paragraph in text.Split(Environment.NewLine))
                foreach (var word in paragraph.Split(splitChar, StringSplitOptions.RemoveEmptyEntries))
                    words.Add(word);
            return words;
        }
    }
}

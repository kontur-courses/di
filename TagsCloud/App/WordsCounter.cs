using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TagsCloud.App
{
    public class WordsCounter
    {
        public Dictionary<string, int> CountWords(IEnumerable<string> words)
        {
            var result = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (result.ContainsKey(word))
                    result[word]++;
                else result.Add(word, 1);
            }
            return result;
        }
    }
}

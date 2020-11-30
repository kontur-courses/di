using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.App
{
    public class WordsConverter : IWordsConverter
    {
        public IEnumerable<string> ConvertWords(IEnumerable<string> words)
        {
            foreach (var word in words)
                yield return ConvertWord(word);
        }

        public string ConvertWord(string word)
        {
            return word.ToLower();
        }
    }
}

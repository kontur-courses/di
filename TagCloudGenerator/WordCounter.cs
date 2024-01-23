using System;
using System.Collections.Generic;
using System.Text;

namespace TagCloudGenerator
{
    public class WordCounter
    {
        public Dictionary<string, int> words;
        
        public Dictionary<string, int> CountWords(string text)
        {
            words = new Dictionary<string, int>();

            foreach (string word in text.Split(' '))
            {
                if (words.ContainsKey(word))
                    words[word]++;
                else words[word] = 1;
            }

            return words;
        }
    }
}

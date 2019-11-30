using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Core;

namespace TagsCloudVisualization.Text
{
    public static class WordReader
    {
        public static Word[] GetAllWords(TextReader textReader)
        {
            var frequency = new Dictionary<string, int>();
            foreach (var word in textReader)
            {
                if (!frequency.ContainsKey(word))
                    frequency[word] = 0;
                frequency[word]++;
            }
            return frequency.Select(x => new Word(x.Key, x.Value)).ToArray();
        }
    }
}
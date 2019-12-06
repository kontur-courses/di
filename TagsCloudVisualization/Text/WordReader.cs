using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Core;

namespace TagsCloudVisualization.Text
{
    public static class WordReader
    {
        public static Word[] GetAllWords(ITextReader textReader, string filepath)
        {
            var wordCount = new Dictionary<string, int>();
            foreach (var word in textReader.GetAllWords(filepath))
            {
                if (!wordCount.ContainsKey(word))
                    wordCount[word] = 0;
                wordCount[word]++;
            }
            return wordCount.Select(x => new Word(x.Key, x.Value)).ToArray();
        }
    }
}
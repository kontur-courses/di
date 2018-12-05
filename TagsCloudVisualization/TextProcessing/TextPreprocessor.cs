using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHunspell;

namespace TagsCloudVisualization
{
    public class TextPreprocessor
    {
        private readonly string[] boringWords;
        
        public TextPreprocessor(string[] boringWords)
        {
            this.boringWords = boringWords;
        }

        public IEnumerable<(string, int)> PreprocessWords(string words)
        {
            return words
                .Split(new string[]{Environment.NewLine}, StringSplitOptions.None)
                .Select(s => s.ToLower())
                .Where(s => !boringWords.Contains(s))
                .GroupBy(s => s)
                .OrderByDescending(s => s.Count())
                .Select(g => (g.Key, g.Count()));
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class WordPreprocessor
    {
        private readonly IEnumerable<string> words;

        public WordPreprocessor(IEnumerable<string> words)
        {
            this.words = words;
        }

        public WordPreprocessor Exclude(IEnumerable<string> stopWords)
        {
            var newWords = words.Where(w => !stopWords.Contains(w));
            return new WordPreprocessor(newWords);
        }

        public WordPreprocessor ToLowercase()
        {
            var newWords = words.Select(w => w.ToLower());
            return new WordPreprocessor(newWords);
        }



        public IEnumerable<Tag> Compile()
        {
            throw new NotImplementedException();
        }
    }
}
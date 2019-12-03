using System;
using System.Collections.Generic;

namespace TagsCloudTextPreparation
{
    public class TextPreparerConfig
    {
        private readonly HashSet<string> excludedWords = new HashSet<string>();

        public string SplitPattern { get; set; } = @"\W";

        //todo - Excluding parts of speech
        //todo UsingWordsCase - (lower, upper or title)

        public TextPreparerConfig Excluding(IEnumerable<string> words)
        {
            if (words == null)
                throw new ArgumentException("Collection of words to exclude can't be null");
            foreach (var word in words)
                if (!excludedWords.Contains(word))
                    excludedWords.Add(word);
            return this;
        }

        public TextPreparerConfig UsingWordsSplitPattern(string pattern)
        {
            SplitPattern = pattern;
            return this;
        }
        
        public bool IsWordExcluded(string word) => excludedWords.Contains(word);
    }
}
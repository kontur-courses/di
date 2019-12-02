using System.Collections.Generic;

namespace TagsCloudTextPreparation
{
    public class TextPreparerConfig
    {
        private readonly HashSet<string> excludedWords = new HashSet<string>();
        
        //todo - Excluding parts of speech
        //todo UsingWordsCase - (lower, upper or title)
        
        public TextPreparerConfig Excluding(IEnumerable<string> words)
        {
            foreach (var word in words)
                if (!excludedWords.Contains(word))
                    excludedWords.Add(word);
            return this;
        }
        
       
        public bool IsWordExcluded(string word) => excludedWords.Contains(word);
    }
}
using System.Collections.Generic;

namespace TextConfiguration
{
    public class DefaultTextPreprocessingSettings : ITextPreprocessingSettings
    {
        private readonly List<string> excludedWords = 
            new List<string>() { "boring", "bored", "boooored", "зевнул" };

        public bool TryPreprocessWord(string word, out string preprocessedWord)
        {
            preprocessedWord = null;
            if (word is null)
                return false;
            preprocessedWord = word.ToLower();
            return !excludedWords.Contains(preprocessedWord);
        }
    }
}

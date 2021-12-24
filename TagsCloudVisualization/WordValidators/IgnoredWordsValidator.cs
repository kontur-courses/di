using System.Collections.Generic;

namespace TagsCloudVisualization.WordValidators
{
    internal class IgnoredWordsValidator : IWordValidator
    {
        private HashSet<string> boringWords;
        
        public IgnoredWordsValidator()
        {
            boringWords = new HashSet<string> {"в", "на", "что", "ты", "я", "вы", "он", "на", "из", "от", "за", "для"};
        }

        public void SetIgnoreWords(IEnumerable<string> invalidWords)
        {
            boringWords = new HashSet<string>(invalidWords);
        }

        public bool Validate(string word)
        {
            return !boringWords.Contains(word);
        }
    }
}
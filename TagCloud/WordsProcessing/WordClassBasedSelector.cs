using System.Collections.Generic;

namespace TagCloud.WordsProcessing
{
    public class WordClassBasedSelector : IWordSelector
    {
        private readonly IWordClassIdentifier wordClassIdentifier;
        private readonly HashSet<WordClass> wordClasses;
        private readonly bool isBlackList;

        public WordClassBasedSelector(
            IWordClassIdentifier wordClassIdentifier, 
            HashSet<WordClass> wordClasses,
            bool isBlackList = true)
        {
            this.wordClassIdentifier = wordClassIdentifier;
            this.wordClasses = wordClasses;
            this.isBlackList = isBlackList;
        }

        public bool IsSelectedWord(string word)
        {
            var wordClass = wordClassIdentifier.GetWordClass(word);
            if (isBlackList)
                return !wordClasses.Contains(wordClass);
            return wordClasses.Contains(wordClass);
        }
    }
}

using System.Collections.Generic;

namespace TagsCloudContainer.Word_Counting
{
    public class WordFilter : IWordFilter
    {
        private readonly HashSet<string> excludedWords;
        private readonly HashSet<string> excludedPartsOfSpeech;

        public WordFilter(HashSet<string> excludedWords, HashSet<string> excludedPartsOfSpeech)
        {
            this.excludedWords = excludedWords;
            this.excludedPartsOfSpeech = excludedPartsOfSpeech;
        }

        public bool IsExcluded(string word)
        {
            return excludedWords.Contains(word) || excludedPartsOfSpeech.Contains(GetPartOfSpeech(word));
        }

        private string GetPartOfSpeech(string word)
        {
            return "Существительное";
        }
    }
}
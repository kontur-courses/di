using System.Collections.Generic;

namespace TagCloud.WordsPreprocessing.WordsSelector
{
    public class WordSelector
    {
        public int IgnoreWordsWithLengthLessThan { get; set; }

        public HashSet<string> IgnoredWords { get; }
        public HashSet<SpeechPart> IgnoredSpeechParts { get; }

        public WordSelector(HashSet<string> ignoredWords, HashSet<SpeechPart> ignoredSpeechParts)
        {
            IgnoredWords = ignoredWords;
            IgnoredSpeechParts = ignoredSpeechParts;
        }

        public bool CanUseThisWord(Word word)
        {
            return word.Value.Length >= IgnoreWordsWithLengthLessThan && !IgnoredWords.Contains(word.Value) &&
                   !IgnoredSpeechParts.Contains(word.PartOfSpeech);
        }
    }
}
using System.Collections.Generic;

namespace TagCloud.WordsPreprocessing.WordsSelector
{
    public class WordSelectorSettings
    {
        public int IgnoreWordsWithLengthLessThan { get; set; }
        public IReadOnlyCollection<string> IgnoredWords => ignoredWords;
        public IReadOnlyCollection<SpeechPart> IgnoredSpeechParts => ignoredSpeechParts;

        private readonly HashSet<string> ignoredWords;
        private readonly HashSet<SpeechPart> ignoredSpeechParts;

        public WordSelectorSettings()
        {
            ignoredWords = new HashSet<string>();
            ignoredSpeechParts = new HashSet<SpeechPart>();
        }

        public bool CanUseThisWord(Word word)
        {
            return word.Value.Length >= IgnoreWordsWithLengthLessThan && !ignoredWords.Contains(word.Value) &&
                   !ignoredSpeechParts.Contains(word.PartOfSpeech);
        }

        public bool AddIgnoredWord(string word)
        {
            return ignoredWords.Add(word);
        }

        public bool RemoveIgnoredWord(string word)
        {
            return ignoredWords.Remove(word);
        }

        public bool AddIgnoredSpeechPart(SpeechPart speechPart)
        {
            return ignoredSpeechParts.Add(speechPart);
        }

        public bool RemoveIgnoredSpeechPart(SpeechPart speechPart)
        {
            return ignoredSpeechParts.Remove(speechPart);
        }
    }
}
using System;

namespace TagCloudCreation
{
    public class WordInfo
    {
        public WordInfo(string word, int count)
        {
            Word = word;
            Count = count;
        }

        public WordInfo With(string changedWord)
        {
            return new WordInfo(changedWord, Count);
        }

        public WordInfo With(Func<string, string> wordChanger)
        {
            return With(wordChanger(Word));
        }

        public string Word { get; }
        public int Count { get; }
    }
}

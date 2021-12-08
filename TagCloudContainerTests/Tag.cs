using System.Collections.Generic;

namespace TagCloudContainerTests
{
    public class Tag
    {
        public readonly string Word;
        public readonly int Count;

        public Tag(string word, int count)
        {
            Word = word;
            Count = count;
        }

        public IEnumerable<string> GetWords()
        {
            for (var i = 0; i < Count; i++)
                yield return Word;
        }
    }
}
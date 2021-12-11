using System.Collections.Generic;

namespace TagsCloudContainer.Common
{
    public class SimpleTag
    {
        public readonly string Word;
        public readonly int Count;

        public SimpleTag(string word, int count)
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
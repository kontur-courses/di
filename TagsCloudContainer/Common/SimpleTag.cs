using System;
using System.Collections.Generic;

namespace TagsCloudContainer.Common
{
    public class SimpleTag
    {
        public readonly string Word;
        public readonly int Count;

        public SimpleTag(string word, int count)
        {
            if(word.Length < 1)
                throw new ArgumentException("Word length can`t be less than 1");
            Word = word;
            if (count < 1)
                throw new ArgumentException("Word count can`t be less than 1");
            Count = count;
        }

        public IEnumerable<string> GetWords()
        {
            for (var i = 0; i < Count; i++)
                yield return Word;
        }
    }
}
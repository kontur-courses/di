using System.Collections.Generic;

namespace TagsCloudVisualization.Statistics
{
    public record WordCount
    {
        public WordCount(string word, int count)
        {
            Word = word;
            Count = count;
        }

        public string Word { get; }
        public int Count { get; }

        public static WordCount Create(KeyValuePair<string, int> pair)
        {
            return new WordCount(pair.Key, pair.Value);
        }

        public void Deconstruct(out string word, out int count)
        {
            (word, count) = (Word, Count);
        }
    }
}
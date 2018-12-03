using System.Collections.Generic;

namespace TagsCloudVisualization.Utils
{
    public class WordStatistics
    {
        public readonly string Word;
        public readonly int Count;

        public WordStatistics(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }

    public class Statistics
    {
        public readonly IReadOnlyList<WordStatistics> OrderedWordsStatistics;
        public readonly int AllWordsCount;

        public Statistics(IReadOnlyList<WordStatistics> orderedWordsStatistics, int allWordsCount)
        {
            OrderedWordsStatistics = orderedWordsStatistics;
            AllWordsCount = allWordsCount;
        }
    }
}

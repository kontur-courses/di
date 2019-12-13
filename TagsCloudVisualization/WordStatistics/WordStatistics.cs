using System.Collections.Generic;
using TagsCloudVisualization.Core;

namespace TagsCloudVisualization.WordStatistics
{
    public class WordStatistics
    {
        public readonly StatisticsType Type;
        public readonly Word Word;

        public WordStatistics(Word word, StatisticsType type)
        {
            Word = word;
            Type = type;
        }

        public override bool Equals(object obj)
        {
            return obj is WordStatistics statistics &&
                   EqualityComparer<Word>.Default.Equals(Word, statistics.Word) &&
                   Type == statistics.Type;
        }

        public override int GetHashCode()
        {
            var hashCode = -1903944686;
            hashCode = hashCode * -1521134295 + EqualityComparer<Word>.Default.GetHashCode(Word);
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }
    }
}
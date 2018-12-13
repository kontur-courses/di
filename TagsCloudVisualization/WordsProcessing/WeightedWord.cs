using System;

namespace TagsCloudVisualization.WordsProcessing
{
    public class WeightedWord : IComparable<WeightedWord>
    {
        public string Word { get; }
        public int Weight { get; }

        public WeightedWord(string word, int weight)
        {
            Word = word;
            Weight = weight;
        }

        public int CompareTo(WeightedWord otherWeightedWord)
        {
            return otherWeightedWord == null ? 1 : Weight.CompareTo(otherWeightedWord.Weight);
        }
    }
}
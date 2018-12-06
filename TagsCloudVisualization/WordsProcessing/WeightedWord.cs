using System;

namespace TagsCloudVisualization
{
    public class WeightedWord : IComparable
    {
        public string Word { get; }
        public int Weight { get; }

        public WeightedWord(string word, int weight)
        {
            Word = word;
            Weight = weight;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (obj is WeightedWord otherWeightedWord) 
                return Weight.CompareTo(otherWeightedWord.Weight);
            else
                throw new ArgumentException("Object is not a Temperature");
        }
    }
}
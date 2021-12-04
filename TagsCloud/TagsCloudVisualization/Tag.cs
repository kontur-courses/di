using System;

namespace TagsCloudVisualization
{
    public readonly struct Tag
    {
        public readonly string Word;
        public readonly float Weight;

        public Tag(string word, float weight)
        {
            if (weight is <= 0 or > 1)
                throw new ArgumentException($"{nameof(weight)} expected be in (0, 1]");
            Word = word;
            Weight = weight;
        }
    }
}
﻿namespace TagCloud.Infrastructure.WordWeigher;

public class WordWeigher : IWordWeigher
{
    public Dictionary<string, int> GetWeightedWords(IEnumerable<string> lines)
    {
        return lines.GroupBy(s => s.Trim().ToLowerInvariant())
            .ToDictionary(g => g.Key, g => g.Count());
    }
}
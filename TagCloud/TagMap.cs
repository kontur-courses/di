﻿namespace TagCloud;

public class TagMap
{
    public int UniqueWordCount { get; private set; }
    public IReadOnlyDictionary<string, int> FrequencyMap => frequencyMap;
    private readonly Dictionary<string, int> frequencyMap = new();

    public void AddWord(string word)
    {
        if (!frequencyMap.ContainsKey(word))
        {
            frequencyMap[word] = 0;
            UniqueWordCount++;
        }
        
        frequencyMap[word]++;
    }
}
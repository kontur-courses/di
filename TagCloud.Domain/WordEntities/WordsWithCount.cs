namespace TagCloud.Domain.WordEntities;

public class WordsWithCount
{
    public WordsWithCount(int minCount, int maxCount, WordWithCount[] words)
    {
        MinCount = minCount;
        MaxCount = maxCount;
        Words = words;
    }

    public int MinCount { get; }
    public int MaxCount { get; }
    public WordWithCount[] Words { get; }
    public int CountWindow => MaxCount - MinCount == 0 ? 1 : MaxCount - MinCount;
}
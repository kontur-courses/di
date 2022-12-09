namespace TagCloud.WordSizingAlgorithm;

public class RelativeWordSizingAlgorithm : IWordSizingAlgorithm
{
    private readonly int baseWordSize;

    public RelativeWordSizingAlgorithm() : this(12)
    { }
    
    public RelativeWordSizingAlgorithm(int baseWordSize)
    {
        this.baseWordSize = baseWordSize;
    }
    
    public int GetWordSize(string word, TagMap tagMap)
    {
        return (int)Math.Round(tagMap[word] / (double)tagMap.TotalWordCount * baseWordSize);
    }

    public Tag[] GetTagSizes(TagMap tagMap)
    {
        var result = new Tag[tagMap.UniqueWordCount];
        var i = 0;
        foreach (var word in tagMap.FrequencyMap.Keys)
        {
            var size = GetWordSize(word, tagMap);
            result[i++] = new Tag(word, size);
        }

        return result;
    }
}
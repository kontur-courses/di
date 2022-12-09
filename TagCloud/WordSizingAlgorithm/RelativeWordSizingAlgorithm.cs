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
}
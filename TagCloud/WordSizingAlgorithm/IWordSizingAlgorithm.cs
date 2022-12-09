namespace TagCloud.WordSizingAlgorithm;

public interface IWordSizingAlgorithm
{
    public int GetWordSize(string word, TagMap tagMap);

    public Tag[] GetTagSizes(TagMap tagMap);
}
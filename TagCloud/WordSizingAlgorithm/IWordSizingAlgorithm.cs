namespace TagCloud.WordSizingAlgorithm;

public interface IWordSizingAlgorithm
{
    public int GetWordSize(string word, TagMap tagMap);
}
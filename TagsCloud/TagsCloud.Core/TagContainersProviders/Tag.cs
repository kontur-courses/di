namespace TagsCloud.Core.TagContainersProviders;

public class Tag
{
    public Tag(string word, int count)
    {
        Word = word;
        Count = count;
    }

    public string Word { get; }
    public int Count { get; }

    public override string ToString()
    {
	    return $"{Word}:{{{Count}}}";
    }
}
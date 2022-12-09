namespace TagsCloud.Core;

public class Tag
{
	public Tag(string word, int count)
	{
		Word = word;
		Count = count;
	}

	public string Word { get; }
	public int Count { get; set; }
}
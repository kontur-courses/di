namespace TagCloud.Domain.WordEntities;

public class WordWithCount
{
    public WordWithCount(string text, int count)
    {
        Text = text;
        Count = count;
    }

    public string Text { get; }
    public int Count { get; }
}
using TagCloud.Abstractions;

namespace TagCloud;

public class Tag : ITag
{
    public Tag(string text, int weight)
    {
        Text = text;
        Weight = weight;
    }

    public string Text { get; }

    public int Weight { get; }
}
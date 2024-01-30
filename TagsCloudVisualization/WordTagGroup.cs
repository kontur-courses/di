namespace TagsCloudVisualization;

public class WordTagGroup
{
    public WordTagGroup(string text, int count)
    {
        WordInfo.Text = text;
        Count = count;
    }

    public WordInfo WordInfo { get; } = new();
    public VisualInfo VisualInfo { get; } = new();
    public int Count { get; }

    public override int GetHashCode()
    {
        return WordInfo.Text.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return obj is WordTagGroup group
               && group.WordInfo.Text.Equals(WordInfo.Text, StringComparison.OrdinalIgnoreCase);
    }
}
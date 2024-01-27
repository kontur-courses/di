namespace TagsCloudVisualization;

public class WordTagGroup
{
    public WordInfo WordInfo { get; set; } = new();
    public VisualInfo VisualInfo { get; set; } = new();
    public int Count { get; }

    public WordTagGroup(string text, int count)
    {
        WordInfo.Text = text;
        Count = count;
    }
    
    public override int GetHashCode()
    {
        return WordInfo.Text.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return obj is WordTagGroup group && group.WordInfo.Text.Equals(WordInfo.Text);
    }
}
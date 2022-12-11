namespace TagCloud.Abstractions;

public interface ITag
{
    public string Text { get; }
    public int Weight { get; }
}
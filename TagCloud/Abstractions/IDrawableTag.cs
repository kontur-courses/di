using System.Drawing;

namespace TagCloud.Abstractions;

public interface IDrawableTag
{
    public ITag Tag { get; }
    public int FontSize { get; }
    public Point Location { get; }
}
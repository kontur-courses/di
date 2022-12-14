using System.Drawing;
using TagCloud.Abstractions;

namespace TagCloud;

public class DrawableTag : IDrawableTag
{
    public DrawableTag(ITag tag, int fontSize, Point location)
    {
        Tag = tag;
        FontSize = fontSize;
        Location = location;
    }

    public ITag Tag { get; }
    public int FontSize { get; }
    public Point Location { get; }
}
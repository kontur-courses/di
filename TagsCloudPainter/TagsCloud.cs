using System.Drawing;
using TagsCloudPainter.Tags;

namespace TagsCloudPainter;

public class TagsCloud
{
    public Point Center { get; private set; }
    public Dictionary<Tag, Rectangle> Tags { get; }

    public TagsCloud(Point center, Dictionary<Tag, Rectangle> tags)
    {
        Center = center;
        Tags = tags ?? [];
    }

    public void AddTag(Tag tag, Rectangle rectangle)
    {
        Tags.Add(tag, rectangle);
    }

    public int GetWidth()
    {
        return Tags.Values.Max(rect => rect.X) - Tags.Values.Min(rect => rect.X);
    }

    public int GetHeight()
    {
        return Tags.Values.Max(rect => rect.Y) - Tags.Values.Min(rect => rect.Y);
    }
}
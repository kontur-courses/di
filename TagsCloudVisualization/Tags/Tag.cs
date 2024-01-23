using System.Drawing;

namespace TagsCloudVisualization.Tags;

public class Tag : ITag
{
    public string Content { get; }
    public int Size { get; }
    public Rectangle Rectangle { get; }

    public Tag(string content, int size, Rectangle rectangle)
    {
        Content = content;
        Size = size;
        Rectangle = rectangle;
    }
}
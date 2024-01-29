using System.Drawing;

namespace TagsCloudVisualization.Tags;

public interface ITag
{
    string Content { get; }
    public int Size { get; }
    Rectangle Rectangle { get; }
    FontFamily Font { get; }
}
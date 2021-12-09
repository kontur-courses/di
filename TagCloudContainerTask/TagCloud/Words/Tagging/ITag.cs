using System.Drawing;

namespace TagCloud.Words.Tagging
{
    public interface ITag
    {
        string Word { get; }
        float KegelSize { get; }
        Size WordRectangleSize { get; }
    }
}
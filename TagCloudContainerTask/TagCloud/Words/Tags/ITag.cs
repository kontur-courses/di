using System.Drawing;

namespace TagCloud.Words.Tags
{
    public interface ITag
    {
        string Word { get; }
        float WordEmSize { get; }
        Rectangle WordOuterRectangle { get; set; }
    }
}
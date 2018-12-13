using System.Drawing;

namespace TagsCloudContainer.Tag
{
    public interface ITag
    {
        string Value { get; }
        Font Font { get; }
    }
}
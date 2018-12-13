using System.Drawing;

namespace TagsCloudContainer
{
    public interface ITagsCloudFactory
    {
        ITagsCloud CreateTagsCloud();
    }
}
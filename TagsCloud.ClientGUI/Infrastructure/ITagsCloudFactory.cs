using TagsCloud.Common;

namespace TagsCloud.ClientGUI.Infrastructure
{
    public interface ITagsCloudFactory
    {
        ICircularCloudLayouter Create();
    }
}
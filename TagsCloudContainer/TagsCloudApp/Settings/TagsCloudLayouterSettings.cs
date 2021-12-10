using TagsCloudContainer.DependencyInjection;
using TagsCloudContainer.Layout;
using TagsCloudContainer.Settings;

namespace TagsCloudApp.Settings
{
    public class TagsCloudLayouterSettings : ITagsCloudLayouterSettings
    {
        public ITagsCloudLayouter TagsCloudLayouter { get; }

        public TagsCloudLayouterSettings(IServiceResolver<TagsCloudLayouterType, ITagsCloudLayouter> resolver)
        {
            TagsCloudLayouter = resolver.GetService(TagsCloudLayouterType.FontBased);
        }
    }
}
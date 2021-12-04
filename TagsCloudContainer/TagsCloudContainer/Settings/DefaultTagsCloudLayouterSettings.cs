using System;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Settings
{
    public interface ITagsCloudLayouterSettings
    {
        ITagsCloudLayouter Layouter { get; set; }
    }

    public class DefaultTagsCloudLayouterSettings : ITagsCloudLayouterSettings
    {
        private readonly Lazy<FontBasedLayouter> fontBasedLayouter;
        private ITagsCloudLayouter layouter;

        public ITagsCloudLayouter Layouter
        {
            get => layouter ?? fontBasedLayouter.Value;
            set => layouter = value;
        }

        public DefaultTagsCloudLayouterSettings(Lazy<FontBasedLayouter> fontBasedLayouter)
        {
            this.fontBasedLayouter = fontBasedLayouter;
        }
    }
}
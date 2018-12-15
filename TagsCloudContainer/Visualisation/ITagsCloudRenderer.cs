using TagsCloudContainer.TagsClouds;
using TagsCloudContainer.Visualisation.Coloring;

namespace TagsCloudContainer.Visualisation
{
    public interface ITagsCloudRenderer
    {
        void RenderIntoFile(ImageSettings imageSettings, IColorManager colorManager, ITagsCloud tagsCloud, bool autosize);
    }
}
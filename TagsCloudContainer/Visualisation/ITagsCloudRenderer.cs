using TagsCloudContainer.TagsClouds;
using TagsCloudContainer.Visualisation.Coloring;

namespace TagsCloudContainer.Visualisation
{
    public interface ITagsCloudRenderer
    {
        void RenderIntoFile(string path, ITagsCloud tagsCloud, IColorManager colorManager, bool autosize);
    }
}
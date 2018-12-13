using TagsCloudContainer.TagsClouds;

namespace TagsCloudContainer.Visualisation
{
    public interface ITagsCloudRenderer
    {
        void RenderIntoFile(string path, ITagsCloud tagsCloud, IColorManager colorManager, bool autosize);
    }
}
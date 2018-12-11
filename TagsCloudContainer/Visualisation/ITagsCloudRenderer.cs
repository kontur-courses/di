namespace TagsCloudContainer.Visualisation
{
    public interface ITagsCloudRenderer
    {
        void RenderIntoFile(string path, ITagsCloud tagsCloud, bool autosize);
    }
}
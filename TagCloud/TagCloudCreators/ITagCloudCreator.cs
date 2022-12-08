using TagCloud.TagCloudVisualizations;

namespace TagCloud.TagCloudCreators
{
    public interface ITagCloudCreator
    {
        TagCloud GenerateTagCloud(ITagCloudVisualizationSettings settings);
    }
}

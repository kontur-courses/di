using TagsCloudVisualization.TagsCloud;

namespace TagsCloudVisualization.InterfacesForSettings
{
    public interface ITagsCloudSettings
    {
        IWordsSettings WordsSettings { get; set; }
        Palette Palette { get; set; }
        IImageSettings ImageSettings { get; set; }
    }
}
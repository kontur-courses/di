using TagsCloudVisualization.InterfacesForSettings;
using TagsCloudVisualization.WordProcessing;

namespace TagsCloudVisualization.TagsCloud
{
    public class TagsCloudSettings : ITagsCloudSettings
    {
        public IWordsSettings WordsSettings { get; set; }
        public Palette Palette { get; set; }
        public IImageSettings ImageSettings { get; set; }

        public TagsCloudSettings(IWordsSettings wordsSettings, Palette palette, IImageSettings imageSettings)
        {
            WordsSettings = wordsSettings;
            Palette = palette;
            ImageSettings = imageSettings;
        }
    }
}
using TagsCloudContainer.Configuration;

namespace TagsCloudContainer.TagsGenerator
{
    public class TagsGeneratorSettings : ITagsGeneratorSettings
    {
        public string FontFamily { get; }

        public TagsGeneratorSettings(IConfiguration configuration)
        {
            FontFamily = configuration.FontFamily;
        }
    }
}
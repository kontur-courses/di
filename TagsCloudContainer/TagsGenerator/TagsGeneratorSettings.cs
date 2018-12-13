using System.Drawing;
using TagsCloudContainer.Configuration;

namespace TagsCloudContainer.TagsGenerator
{
    public class TagsGeneratorSettings : ITagsGeneratorSettings
    {
        public FontFamily FontFamily { get; }

        public TagsGeneratorSettings(IConfiguration configuration)
        {
            FontFamily = configuration.FontFamily;
        }
    }
}
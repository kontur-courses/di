using TagsCloudContainer.Configuration;

namespace TagsCloudContainer.TagFontSizeCalculator
{
    public class TagFontSizeCalculatorSettings : ITagFontSizeCalculatorSettings
    {
        public int MaxFontSize { get; }
        public int MinFontSize { get; }

        public TagFontSizeCalculatorSettings(IConfiguration configuration)
        {
            MaxFontSize = configuration.MaxFontSize;
            MinFontSize = configuration.MinFontSize;
        }
    }
}